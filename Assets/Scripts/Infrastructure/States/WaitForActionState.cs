using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using UnityEngine;
using UserInput;

namespace Infrastructure.States
{
    public class WaitForActionState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInputService _inputService;

        public IGameStateMachine StateMachine { get; set; }
        
        public WaitForActionState(ICoroutineRunner coroutineRunner, IInputService inputService)
        {
            _coroutineRunner = coroutineRunner;
            _inputService = inputService;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(WaitForAction());
        }

        public void Exit()
        {
        }

        private IEnumerator WaitForAction()
        {
            yield return new WaitUntil(() => _inputService.IsPressed);
            StateMachine.Enter<GameLoopState>();
        }
    }
}