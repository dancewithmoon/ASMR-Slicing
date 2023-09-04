using Base.States;
using Deform;
using Infrastructure.Factory;
using Logic.Knife;
using Logic.Slice;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly DeformableManager _deformableManager;

        private SliceMovement _sliceMovement;
        private SliceThrowable _sliceThrowable;

        private KnifeMovementController _knifeMovementController;
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
            _sliceMovement.FinalPositionReached += OnFinalPositionReached;

            _knifeMovementController = _gameFactory.Knife.GetComponent<KnifeMovementController>();
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
            _knifeMovement.OnRelease -= OnKnifeRelease;
            _knifeMovement.OnStopped -= OnKnifeStopped;
            _sliceMovement.FinalPositionReached -= OnFinalPositionReached;
            _knifeSlicing.OnSliced -= OnSliced;
            
            Object.Destroy(_knifeMovementController);
            Object.Destroy(_knifeMovement);
            Object.Destroy(_sliceMovement);
        }

        private void OnSliced(ISliceable sliceable)
        {
            _sliceMovement.FinalPositionReached -= OnFinalPositionReached;

            _gameFactory.SliceableItem = sliceable.Positive;
            SliceMovement newMovement = _gameFactory.SliceableItem.GetComponent<SliceMovement>();
            newMovement.Initialize(_sliceMovement.FinalPosition);
            
            _sliceMovement = newMovement;
            _sliceMovement.FinalPositionReached += OnFinalPositionReached;
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

        private void OnFinalPositionReached()
        {
            StateMachine.Enter<LevelCompletedState>();
        }
    }
}