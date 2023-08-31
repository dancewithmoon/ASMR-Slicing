using Base.States;
using Infrastructure.Factory;
using Logic;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;

        private Movable _movable;

        public IGameStateMachine StateMachine { get; set; }
        
        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            _movable = _gameFactory.SliceableItem.GetComponent<Movable>();
            _movable.Move();
        }

        public void Exit()
        {
        }
    }
}