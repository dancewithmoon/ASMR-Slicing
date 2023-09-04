using UnityEngine;

namespace Logic
{
    public class SliceMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public bool MovementEnabled { get; private set; }
        public Vector3 FinalPosition { get; private set; }
        
        public void Initialize(Vector3 finalPosition)
        {
            FinalPosition = finalPosition;
        }

        public void Move() => MovementEnabled = true;
        
        public void Stop() => MovementEnabled = false;
        
        
        private void Update()
        {
            if (MovementEnabled)
            {
                MoveToNextPosition();
            }
        }

        private void MoveToNextPosition() => 
            transform.position = GetNextPosition();

        private Vector3 GetNextPosition() =>
            Vector3.MoveTowards(transform.position, FinalPosition, _speed * Time.deltaTime);
    }
}
