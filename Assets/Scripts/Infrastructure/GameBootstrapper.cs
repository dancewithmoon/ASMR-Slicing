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
using Services;
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
            IGameFactory gameFactory = new GameFactory(assets, instantiateService, inputService, staticDataService);

            List<IExitableState> states = new List<IExitableState>
            {
                new BootstrapState(sceneLoader, staticDataService),
                new LoadLevelState(sceneLoader, gameFactory, staticDataService),
                new WaitForActionState(this, inputService),
                new GameLoopState(gameFactory, deformableManager)
            };
            IGameStateMachine stateMachine = new GameStateMachine(states);
            
            ServiceLocator.Container.RegisterSingle(sceneLoader);
            ServiceLocator.Container.RegisterSingle(inputService);
            ServiceLocator.Container.RegisterSingle(assets);
            ServiceLocator.Container.RegisterSingle(instantiateService);
            ServiceLocator.Container.RegisterSingle(gameFactory);
            ServiceLocator.Container.RegisterSingle(staticDataService);
            ServiceLocator.Container.RegisterSingle(stateMachine);
            
            _game = new Game(stateMachine);
        }

        public void Start()
        {
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}
