using System;
using DG.Tweening;
using UnityEngine;

namespace Logic.Knife
{
    [RequireComponent(typeof(Rigidbody))]
    public class KnifeMovement : MonoBehaviour
    {
        [Header("Debug only")]
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;
        [SerializeField] private float _releaseDuration;
        
        private Rigidbody _rigidbody;
        private Vector3 _defaultPosition;

        private bool _stopped;
        private bool _released;

        public event Action OnStopped;
        public event Action OnRelease;
        
        public void Initialize(Vector3 direction, float speed, float releaseDuration)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultPosition = transform.position;
            _direction = direction;
            _speed = speed;
            _releaseDuration = releaseDuration;
        }

        public void SetSpeed(float speed) => _speed = speed;

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
            
            _rigidbody.DOMove(_defaultPosition, _releaseDuration)
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
            Vector3.MoveTowards(transform.position, transform.position + _direction, _speed * Time.fixedDeltaTime);
    }
}