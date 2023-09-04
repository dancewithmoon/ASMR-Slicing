using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using Constants;
using Infrastructure.Factory;
using Logic.Knife;
using UI.ScreenService;
using UnityEngine;
using UserInput;

namespace Infrastructure.States
{
    public class LevelCompletedState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IInputService _inputService;
        private readonly IScreenService _screenService;

        public IGameStateMachine StateMachine { get; set; }

        public LevelCompletedState(ICoroutineRunner coroutineRunner, IGameFactory gameFactory, IInputService inputService, IScreenService screenService)
        {
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
            _inputService = inputService;
            _screenService = screenService;
        }

        public void Enter()
        {
            _screenService.Close(ScreenId.GameHudScreen);
            _screenService.Open(ScreenId.LevelCompletedScreen);
            _gameFactory.Knife.GetComponent<KnifeRemoving>().Remove();
            _coroutineRunner.StartCoroutine(WaitForAction());
        }

        public void Exit()
        {
            _screenService.Close(ScreenId.LevelCompletedScreen);
        }
        
        private IEnumerator WaitForAction()
        {
            yield return new WaitUntil(() => _inputService.IsPressed);
            StateMachine.Enter<LoadLevelState, string>(Scenes.TestScene);
        }
    }
}