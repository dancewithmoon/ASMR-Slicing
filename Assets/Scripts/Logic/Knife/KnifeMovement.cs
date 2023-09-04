using System;
using DG.Tweening;
using Infrastructure.StaticData;
using UnityEngine;

namespace Logic.Knife
{
    [RequireComponent(typeof(Rigidbody))]
    public class KnifeMovement : MonoBehaviour
    {
        [Header("Debug only. Edit in runtime")]
        [SerializeField] private KnifeParameters _knifeParameters;
        
        private Rigidbody _rigidbody;
        private Vector3 _defaultPosition;
        private bool _stopped;
        private bool _released;

        public event Action OnStopped;
        public event Action OnRelease;
        
        public void Initialize(KnifeParameters knifeParameters)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultPosition = transform.position;
            _knifeParameters = knifeParameters;
        }

        public void Move()
        {
            if(_stopped || _released)
                return;
            
            _rigidbody.MovePosition(GetNextPosition());
        }

        public void Release()
        {
            if(_released)
                return;

            _stopped = false;
            _released = true;
            
            _rigidbody.DOMove(_defaultPosition, _knifeParameters.ReleaseDuration)
                .OnComplete(() =>
                {
                    _released = false;
                    OnRelease?.Invoke();
                });
        }

        public void Stop()
        {
            _stopped = true;
            OnStopped?.Invoke();
        }

        private Vector3 GetNextPosition() =>
            Vector3.MoveTowards(transform.position, transform.position + _knifeParameters.MoveDirection, _knifeParameters.MoveSpeed * Time.fixedDeltaTime);
    }
}