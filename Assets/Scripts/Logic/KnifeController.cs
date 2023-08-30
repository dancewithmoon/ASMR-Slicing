using UnityEngine;
using UserInput;

namespace Logic
{
    [RequireComponent(typeof(Knife))]
    public class KnifeController : MonoBehaviour
    {
        private IInputService _inputService;
        private Knife _knife;
        
        public void Initialize(IInputService inputService)
        {
            _inputService = inputService;
            _knife = GetComponent<Knife>();
        }

        private void Update()
        {
            if (_inputService.IsPressed)
            {
                _knife.Cut();
                return;
            }
            
            if(_knife.Released)
                return;

            _knife.Release();
        }
    }
}