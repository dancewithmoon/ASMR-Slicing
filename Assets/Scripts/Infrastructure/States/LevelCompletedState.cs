using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using Constants;
using Infrastructure.Factory;
using Logic.Knife;
using UnityEngine;
using UserInput;

namespace Infrastructure.States
{
    public class LevelCompletedState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IInputService _inputService;

        public IGameStateMachine StateMachine { get; set; }

        public LevelCompletedState(ICoroutineRunner coroutineRunner, IGameFactory gameFactory, IInputService inputService)
        {
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
            _inputService = inputService;
        }

        public void Enter()
        {
            _gameFactory.Knife.GetComponent<KnifeRemoving>().Remove();
            _coroutineRunner.StartCoroutine(WaitForAction());
        }

        public void Exit()
        {
        }
        
        private IEnumerator WaitForAction()
        {
            yield return new WaitUntil(() => _inputService.IsPressed);
            StateMachine.Enter<LoadLevelState, string>(Scenes.TestScene);
        }
    }
}