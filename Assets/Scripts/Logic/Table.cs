using UnityEngine;

namespace Logic
{
    public class Table : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Knife knife = other.GetComponentInParent<Knife>();
            
            if(knife == null)
                return;
            
            knife.Stop();
        }
    }
}