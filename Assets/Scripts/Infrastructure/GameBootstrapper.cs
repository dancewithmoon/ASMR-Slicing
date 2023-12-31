using System.Collections.Generic;
using Base;
using Base.AssetManagement;
using Base.Instantiating;
using Base.Services;
using Base.Services.CoroutineRunner;
using Base.States;
using Deform;
using Infrastructure.Factory;
using Infrastructure.States;
using Infrastructure.StaticData;
using UI.Factory;
using UI.ScreenService;
using UI.StaticData;
using UnityEngine;
using UserInput;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private static GameBootstrapper _instance;
        
        private Game _game;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Application.targetFrameRate = 60;
            DontDestroyOnLoad(this);
            _instance = this;
            
            RegisterServices();
        }

        private void RegisterServices()
        {
            SceneLoader sceneLoader = new SceneLoader(this);

            IInputService inputService = Application.isEditor ? new StandaloneInputService() : new MobileInputService();

            DeformableManager deformableManager = DeformableManager.GetDefaultManager(true);

            IAssets assets = new ResourcesAssets();
            IInstantiateService instantiateService = new InstantiateService();

            IStaticDataService staticDataService = new StaticDataService(assets);
            IUiStaticDataService uiStaticDataService = new UiStaticDataService(assets);
                
            IGameFactory gameFactory = new GameFactory(assets, instantiateService, inputService, staticDataService);
            IUiFactory uiFactory = new UiFactory(assets, instantiateService, uiStaticDataService, staticDataService, gameFactory);

            IScreenService screenService = new ScreenService(uiFactory);
            
            List<IExitableState> states = new List<IExitableState>
            {
                new BootstrapState(sceneLoader, staticDataService, uiStaticDataService),
                new LoadLevelState(sceneLoader, gameFactory, staticDataService, uiFactory, screenService),
                new WaitForActionState(this, inputService, screenService),
                new GameLoopState(this, gameFactory, deformableManager, inputService),
                new LevelCompletedState(this, gameFactory, inputService, screenService)
            };
            IGameStateMachine stateMachine = new GameStateMachine(states);

            _game = new Game(stateMachine);
        }

        public void Start()
        {
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}
