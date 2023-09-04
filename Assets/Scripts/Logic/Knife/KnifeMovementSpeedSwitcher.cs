using Infrastructure.StaticData;
using Logic.Slice;
using UnityEngine;

namespace Logic.Knife
{
    [RequireComponent(typeof(KnifeMovement))]
    public class KnifeMovementSpeedSwitcher : MonoBehaviour
    {
        private KnifeMovement _movement;
        private KnifeParameters _knifeParameters;

        public void Initialize(KnifeParameters knifeParameters)
        {
            _movement = GetComponent<KnifeMovement>();
            _knifeParameters = knifeParameters;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<ISliceable>() != null)
            {
                _movement.SetSpeed(_knifeParameters.SlicingMoveSpeed);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<ISliceable>() != null)
            {
                _movement.SetSpeed(_knifeParameters.MoveSpeed);
            }
        }
    }
}