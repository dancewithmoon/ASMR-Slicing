using UnityEngine;

namespace UserInput
{
    public class StandaloneInputService : IInputService
    {
        public bool IsPressed => Input.GetMouseButton(0);
    }
}