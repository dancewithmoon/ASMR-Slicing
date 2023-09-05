using System.Threading.Tasks;
using BzKovSoft.ObjectSlicer;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Slice
{
    public class BzSliceable : MonoBehaviour, ISliceable
    {
        private IBzSliceable _meshSlicer;
        
        public GameObject Positive { get; private set; }
        public GameObject Negative { get; private set; }

        private BzSliceTryResult _sliceResult;
        
        private void Awake()
        {
            _meshSlicer = GetComponent<IBzSliceable>();
        }

        public async Task Slice(Plane plane)
        {
            _meshSlicer.Slice(plane, OnSlice);
            
            await UniTask.WaitUntil(() => _sliceResult != null);

            Positive = _sliceResult.outObjectPos;
            Negative = _sliceResult.outObjectNeg;

            _sliceResult = null;
            
            await UniTask.NextFrame();
            
            RemoveRedundantSlices(Positive);
            RemoveRedundantSlices(Negative);
        }

        private void OnSlice(BzSliceTryResult result)
        {
            _sliceResult = result;
        }

        private void RemoveRedundantSlices(GameObject slice)
        {
            foreach (Transform obj in slice.GetComponentsInChildren<Transform>())
            {
                if (obj != null && obj.TryGetComponent(out MeshFilter meshFilter) == false)
                {
                    Destroy(obj.gameObject);
                }
            }
        }
    }
}