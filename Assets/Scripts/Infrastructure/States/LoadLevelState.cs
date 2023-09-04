using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Infrastructure.Factory;
using Infrastructure.StaticData;
using UI.Factory;
using UI.ScreenService;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IUiFactory _uiFactory;
        private readonly IScreenService _screenService;

        private Task _preload;

        public IGameStateMachine StateMachine { get; set; }

        public LoadLevelState(SceneLoader sceneLoader, IGameFactory gameFactory, IStaticDataService staticDataService,
            IUiFactory uiFactory, IScreenService screenService)
        {
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _screenService = screenService;
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
            
            _uiFactory.CreateUIRoot();
            
            LevelStaticData levelData = _staticDataService.GetLevelStaticData();
            _gameFactory.CreateKnife(levelData.KnifeSceneData);
            _gameFactory.CreateSliceableItem(levelData.SliceSceneData);

            _screenService.Open(ScreenId.GameHudScreen);
            
            StateMachine.Enter<WaitForActionState>();
        }
    }
}