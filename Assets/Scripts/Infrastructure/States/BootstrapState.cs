using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Constants;
using Infrastructure.StaticData;
using UI.StaticData;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IUiStaticDataService _uiStaticDataService;

        public IGameStateMachine StateMachine { get; set; }

        public BootstrapState(SceneLoader sceneLoader, IStaticDataService staticDataService, IUiStaticDataService uiStaticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _uiStaticDataService = uiStaticDataService;
        }

        public async void Enter()
        {
            await Task.WhenAll(
                _staticDataService.Preload(), 
                _uiStaticDataService.Preload());
            
            _sceneLoader.Load(Scenes.InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            StateMachine.Enter<LoadLevelState, string>(Scenes.TestScene);
        }
    }
}