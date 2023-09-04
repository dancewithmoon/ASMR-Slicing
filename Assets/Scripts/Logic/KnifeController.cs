using UnityEngine;
using UserInput;

namespace Logic
{
    [RequireComponent(typeof(KnifeMovement))]
    public class KnifeController : MonoBehaviour
    {
        private IInputService _inputService;
        private KnifeMovement _movement;
        
        public void Initialize(IInputService inputService)
        {
            _inputService = inputService;
            _movement = GetComponent<KnifeMovement>();
        }

        private void FixedUpdate()
        {
            if (_inputService.IsPressed)
            {
                _movement.Move();
                return;
            }

            _movement.Release();
        }
    }
}