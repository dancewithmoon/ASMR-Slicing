using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction = Vector3.back;
        [SerializeField] private float _throwForce = 5f;
        [SerializeField] private float _destroyTimeout = 5f;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Throw()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(_direction * _throwForce, ForceMode.Force);
            
            Destroy(gameObject, _destroyTimeout);
        }
    }
}