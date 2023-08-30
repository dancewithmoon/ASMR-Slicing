using UnityEngine;

namespace UserInput
{
    public class MobileInputService : IInputService
    {
        public bool IsPressed => Input.touchCount > 0;
    }
}