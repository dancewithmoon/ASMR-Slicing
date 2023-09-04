using UnityEngine;

namespace UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        private void Start()
        {
            Initialize();
            Subscribe();
        }

        private void OnDestroy() => Cleanup();

        protected virtual void Initialize(){}
        protected virtual void Subscribe(){}
        protected virtual void Cleanup(){}
    }
}