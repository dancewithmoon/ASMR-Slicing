using DG.Tweening;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knife : MonoBehaviour
    {
        [SerializeField] private float _cutSpeed;
        [SerializeField] private float _releaseDuration;

        private Rigidbody _rigidbody;
        private Vector3 _defaultPosition;

        public bool Released { get; private set; }
        public bool Stopped { get; private set; }
        
        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultPosition = transform.position;
        }
        
        public void Cut()
        {
            if(Stopped)
                return;
            
            Released = false;
            _rigidbody.MovePosition(GetNextPosition());
        }

        public void Release()
        {
            Released = true;
            Stopped = false;
            _rigidbody.DOMove(_defaultPosition, _releaseDuration);
        }

        public void Stop()
        {
            Stopped = true;
        }

        private Vector3 GetNextPosition() =>
            Vector3.MoveTowards(transform.position, transform.position + Vector3.down, _cutSpeed * Time.deltaTime);
    }
}