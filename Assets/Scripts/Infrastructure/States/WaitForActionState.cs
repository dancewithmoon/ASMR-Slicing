using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using UI.ScreenService;
using UnityEngine;
using UserInput;

namespace Infrastructure.States
{
    public class WaitForActionState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInputService _inputService;
        private readonly IScreenService _screenService;

        public IGameStateMachine StateMachine { get; set; }
        
        public WaitForActionState(ICoroutineRunner coroutineRunner, IInputService inputService, IScreenService screenService)
        {
            _coroutineRunner = coroutineRunner;
            _inputService = inputService;
            _screenService = screenService;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(WaitForAction());
            _screenService.Open(ScreenId.StartScreen);
        }

        public void Exit()
        {
            _screenService.Close(ScreenId.StartScreen);
        }

        private IEnumerator WaitForAction()
        {
            yield return new WaitUntil(() => _inputService.IsPressed);
            StateMachine.Enter<GameLoopState>();
        }
    }
}