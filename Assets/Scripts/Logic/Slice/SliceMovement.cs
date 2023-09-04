using System;
using UnityEngine;

namespace Logic.Slice
{
    public class SliceMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private bool _movementEnabled;
        
        public Vector3 FinalPosition { get; private set; }

        public event Action FinalPositionReached;
        
        public void Initialize(Vector3 finalPosition)
        {
            FinalPosition = finalPosition;
        }

        public void Move() => _movementEnabled = true;
        
        public void Stop() => _movementEnabled = false;
        
        
        private void Update()
        {
            if (_movementEnabled)
            {
                MoveToNextPosition();
            }
        }

        private void MoveToNextPosition()
        {
            transform.position = GetNextPosition();
            if (IsFinalPositionReached())
            {
                FinalPositionReached?.Invoke();
            }
        }

        private Vector3 GetNextPosition() =>
            Vector3.MoveTowards(transform.position, FinalPosition, _speed * Time.deltaTime);

        private bool IsFinalPositionReached() => 
            (transform.position - FinalPosition).sqrMagnitude <= float.Epsilon;
    }
}
