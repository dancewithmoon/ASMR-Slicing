using System;
using System.Collections;
using Infrastructure.StaticData;
using UnityEngine;

namespace Logic
{
    public class KnifeSlicing : MonoBehaviour
    {
        [Header("Debug only. Edit in runtime")]
        [SerializeField] private KnifeParameters _knifeParameters;

        private bool _isSliceAllowed = true;

        public event Action<ISliceable> OnSliced;

        public void Initialize(KnifeParameters knifeParameters)
        {
            _knifeParameters = knifeParameters;
        }
        
        public void AllowSlice()
        {
            _isSliceAllowed = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isSliceAllowed == false)
                return;

            if (other.TryGetComponent(out SliceMovement sliceMovement))
            {
                sliceMovement.Stop();
            }
            
            if (other.TryGetComponent(out ISliceable sliceable))
            {
                Slice(sliceable);
            }
        }

        private async void Slice(ISliceable sliceable)
        {
            _isSliceAllowed = false;
            await sliceable.Slice(GetSlicePlane());
            OnSliced?.Invoke(sliceable);
        }

        private Plane GetSlicePlane()
        {
            Vector3 slicePoint = transform.position + _knifeParameters.SliceDirection;
            Vector3 normal = Vector3.Cross(_knifeParameters.MoveDirection, _knifeParameters.SliceDirection);
            return new Plane(normal, slicePoint);
        }
    }
}