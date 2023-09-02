using System;
using DG.Tweening;
using Infrastructure.StaticData;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Rigidbody))]
    public class KnifeMovement : MonoBehaviour
    {
        [Header("Debug only. Edit in runtime")]
        [SerializeField] private KnifeParameters _knifeParameters;
        
        private Rigidbody _rigidbody;
        private Vector3 _defaultPosition;
        private bool _stopped;

        public event Action OnStopped;
        public event Action OnRelease;

        public bool Released { get; private set; }

        public void Initialize(KnifeParameters knifeParameters)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultPosition = transform.position;
            _knifeParameters = knifeParameters;
        }

        public void Move()
        {
            if(_stopped)
                return;
            
            Released = false;
            _rigidbody.MovePosition(GetNextPosition());
        }

        public void Release()
        {
            Released = true;
            _stopped = false;
            _rigidbody.DOMove(_defaultPosition, _knifeParameters.ReleaseDuration).OnComplete(() => OnRelease?.Invoke());
        }

        public void Stop()
        {
            _stopped = true;
            OnStopped?.Invoke();
        }

        private Vector3 GetNextPosition() =>
            Vector3.MoveTowards(transform.position, transform.position + _knifeParameters.MoveDirection, _knifeParameters.MoveSpeed * Time.deltaTime);
    }
}