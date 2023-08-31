using UnityEngine;

namespace Logic
{
    public class Movable : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;

        public bool MovementEnabled { get; private set; }

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
            Vector3.MoveTowards(transform.position, 
                transform.position + _direction, _speed * Time.deltaTime);
    }
}
