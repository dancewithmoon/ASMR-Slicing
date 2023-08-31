using UnityEngine;

namespace Logic
{
    public class Table : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            KnifeMovement knife = other.GetComponentInParent<KnifeMovement>();
            
            if(knife == null)
                return;
            
            knife.Stop();
        }
    }
}