using System;
using Infrastructure.StaticData;
using Logic.Slice;
using UnityEngine;

namespace Logic.Knife
{
    public class KnifeSlicing : MonoBehaviour
    {
        [Header("Runtime only")]
        [SerializeField] private KnifeParameters _knifeParameters;

        private bool _isSliceAllowed = true;

        public event Action OnSliceStarted;
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

            ISliceable sliceable = other.GetComponentInParent<ISliceable>();
            if (sliceable != null)
            {
                OnSliceStarted?.Invoke();
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