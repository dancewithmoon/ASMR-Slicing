using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using Deform;
using Infrastructure.Factory;
using Logic.Knife;
using Logic.Slice;
using UnityEngine;
using UserInput;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly DeformableManager _deformableManager;
        private readonly IInputService _inputService;

        private SliceMovement _sliceMovement;
        private SliceThrowable _sliceThrowable;

        private KnifeInput _knifeInput;
        private KnifeMovement _knifeMovement;
        private KnifeMovementSpeedSwitcher _knifeMovementSpeedSwitcher;
        private KnifeSlicing _knifeSlicing;
        private KnifeDeforming _knifeDeforming;

        private bool _sliceFinished = true;

        public IGameStateMachine StateMachine { get; set; }
        
        public GameLoopState(ICoroutineRunner coroutineRunner, IGameFactory gameFactory, DeformableManager deformableManager, IInputService inputService)
        {
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
            _deformableManager = deformableManager;
            _inputService = inputService;
        }
        
        public void Enter()
        {
            _deformableManager.update = false;
            
            _sliceMovement = _gameFactory.SliceableItem.GetComponent<SliceMovement>();
            _sliceMovement.FinalPositionReached += OnFinalPositionReached;

            _knifeInput = _gameFactory.Knife.GetComponent<KnifeInput>();
            _knifeMovement = _gameFactory.Knife.GetComponent<KnifeMovement>();
            _knifeMovementSpeedSwitcher = _gameFactory.Knife.GetComponent<KnifeMovementSpeedSwitcher>();
            _knifeSlicing = _gameFactory.Knife.GetComponent<KnifeSlicing>();
            _knifeDeforming = _gameFactory.Knife.GetComponent<KnifeDeforming>();

            _knifeSlicing.OnSliceStarted += OnSliceStarted;
            _knifeSlicing.OnSliced += OnSliced;
            _knifeMovement.OnRelease += OnKnifeRelease;
            _knifeMovement.OnStopped += OnKnifeStopped;
            
            _sliceMovement.Move();
        }

        public void Exit()
        {
            _knifeSlicing.OnSliceStarted -= OnSliceStarted;
            _knifeSlicing.OnSliced -= OnSliced;
            _knifeMovement.OnRelease -= OnKnifeRelease;
            _knifeMovement.OnStopped -= OnKnifeStopped;

            Object.Destroy(_knifeInput);
            Object.Destroy(_knifeMovementSpeedSwitcher);
            Object.Destroy(_knifeMovement);
            Object.Destroy(_sliceMovement);
        }

        private void OnSliceStarted()
        {
            _sliceMovement.Stop();
            _sliceFinished = false;
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
            _sliceMovement.FinalPositionReached -= OnFinalPositionReached;
            
            if (_inputService.IsPressed)
            {
                _coroutineRunner.StartCoroutine(WaitForSliceFinish());
            }
            else
            {
                StateMachine.Enter<LevelCompletedState>();
            }
        }

        private IEnumerator WaitForSliceFinish()
        {
            yield return new WaitUntil(() => _knifeMovement.Stopped || (_knifeMovement.Released && _sliceFinished));
            StateMachine.Enter<LevelCompletedState>();
        }
    }
}