using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Infrastructure.Factory;
using Infrastructure.StaticData;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;

        private Task _preload;

        public IGameStateMachine StateMachine { get; set; }

        public LoadLevelState(SceneLoader sceneLoader, IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.CleanUp();
            _preload = Preload();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private async Task Preload()
        {
            await Task.WhenAll(_gameFactory.Preload());
        }

        private async void OnLoaded()
        {
            await _preload;
            
            LevelStaticData levelData = _staticDataService.GetLevelStaticData();
            _gameFactory.CreateKnife(levelData.KnifeSceneData);
            _gameFactory.CreateSliceableItem(levelData.SliceSceneData);
            
            StateMachine.Enter<WaitForActionState>();
        }
    }
}