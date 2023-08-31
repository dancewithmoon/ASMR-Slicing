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

        private void Update()
        {
            if (_inputService.IsPressed)
            {
                _movement.Move();
                return;
            }
            
            if(_movement.Released)
                return;

            _movement.Release();
        }
    }
}