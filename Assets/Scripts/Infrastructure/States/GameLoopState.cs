using Base.States;
using Deform;
using Infrastructure.Factory;
using Logic;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly DeformableManager _deformableManager;

        private SliceMovement _sliceMovement;
        private SliceThrowable _sliceThrowable;

        private KnifeMovement _knifeMovement;
        private KnifeSlicing _knifeSlicing;
        private KnifeDeforming _knifeDeforming;

        private bool _sliceFinished;
        
        public IGameStateMachine StateMachine { get; set; }
        
        public GameLoopState(IGameFactory gameFactory, DeformableManager deformableManager)
        {
            _gameFactory = gameFactory;
            _deformableManager = deformableManager;
        }
        
        public void Enter()
        {
            _deformableManager.update = false;
            
            _sliceMovement = _gameFactory.SliceableItem.GetComponent<SliceMovement>();

            _knifeMovement = _gameFactory.Knife.GetComponent<KnifeMovement>();
            _knifeSlicing = _gameFactory.Knife.GetComponent<KnifeSlicing>();
            _knifeDeforming = _gameFactory.Knife.GetComponent<KnifeDeforming>();

            _knifeSlicing.OnSliced += OnSliced;
            _knifeMovement.OnRelease += OnKnifeRelease;
            _knifeMovement.OnStopped += OnKnifeStopped;
            
            _sliceMovement.Move();
        }

        public void Exit()
        {
            _knifeSlicing.OnSliced -= OnSliced;
            _knifeMovement.OnRelease -= OnKnifeRelease;
            _knifeMovement.OnStopped -= OnKnifeStopped;
        }

        private void OnSliced(ISliceable sliceable)
        {
            SliceMovement newMovement = sliceable.Positive.GetComponent<SliceMovement>();
            newMovement.Initialize(_sliceMovement.FinalPosition);
            _sliceMovement = newMovement;
            _sliceMovement.Stop();
            
            _sliceThrowable = sliceable.Negative.GetComponent<SliceThrowable>();
            
            _deformableManager.update = true;
            _knifeDeforming.StartDeformation(sliceable.Negative.GetComponent<SliceDeformable>());
        }
        
        private void OnKnifeRelease()
        {
            if (_sliceFinished == false) 
                return;
            
            _sliceFinished = false;
            _sliceMovement.Move();
            _knifeSlicing.AllowSlice();
        }
        
        private void OnKnifeStopped()
        {
            if (_sliceThrowable == null) 
                return;
            
            _sliceFinished = true;
            _knifeDeforming.StopDeformation();
            _deformableManager.update = false;
            _sliceThrowable.Throw();
        }
    }
}