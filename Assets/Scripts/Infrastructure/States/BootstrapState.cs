using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Infrastructure.StaticData;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string TestScene = "TestScene";
        
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public IGameStateMachine StateMachine { get; set; }

        public BootstrapState(SceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public async void Enter()
        {
            await Task.WhenAll(_staticDataService.Preload());
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            StateMachine.Enter<LoadLevelState, string>(TestScene);
        }
    }
}