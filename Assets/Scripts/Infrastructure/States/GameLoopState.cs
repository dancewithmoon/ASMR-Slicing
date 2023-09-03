using Base.States;
using Infrastructure.Factory;
using Logic;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;

        private Movable _movable;
        private KnifeSlicing _knifeSlicing;
        private KnifeMovement _knifeMovement;
        private Throwable _slice;

        private bool _sliceFinished;
        
        public IGameStateMachine StateMachine { get; set; }
        
        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            _movable = _gameFactory.SliceableItem.GetComponent<Movable>();
            _knifeSlicing = _gameFactory.Knife.GetComponent<KnifeSlicing>();
            _knifeMovement = _gameFactory.Knife.GetComponent<KnifeMovement>();

            _knifeSlicing.OnSliced += OnSliced;
            _knifeMovement.OnRelease += OnKnifeRelease;
            _knifeMovement.OnStopped += OnKnifeStopped;
            
            _movable.Move();
        }

        public void Exit()
        {
            _knifeSlicing.OnSliced -= OnSliced;
            _knifeMovement.OnRelease -= OnKnifeRelease;
            _knifeMovement.OnStopped -= OnKnifeStopped;
        }

        private void OnSliced(ISliceable sliceable)
        {
            _movable.Stop();
            
            _movable = sliceable.Positive.GetComponent<Movable>();
            _movable.Stop();
            
            _slice = sliceable.Negative.GetComponent<Throwable>();
        }
        
        private void OnKnifeRelease()
        {
            if (_sliceFinished == false) 
                return;
            
            _sliceFinished = false;
            _movable.Move();
            _knifeSlicing.AllowSlice();
        }
        
        private void OnKnifeStopped()
        {
            if (_slice == null) 
                return;
            
            _sliceFinished = true;
            _slice.Throw();
        }
    }
}