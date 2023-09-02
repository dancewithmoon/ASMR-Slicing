using System;
using System.Threading.Tasks;
using BzKovSoft.ObjectSlicer;
using UnityEngine;

namespace Logic
{
    public class BzSliceable : MonoBehaviour, ISliceable
    {
        private IBzMeshSlicer _meshSlicer;
        
        public GameObject Positive { get; private set; }
        public GameObject Negative { get; private set; }
        
        private void Awake()
        {
            _meshSlicer = GetComponent<IBzMeshSlicer>();
        }

        public async Task Slice(Plane plane)
        {
            BzSliceTryResult result = await _meshSlicer.SliceAsync(plane);

            if (result.sliced == false)
                throw new Exception($"Slice failed: {result.rejectMessage}");

            MeshFilter meshFilter1 = result.resultObjects[0].gameObject.GetComponent<MeshFilter>();
            MeshFilter meshFilter2 = result.resultObjects[1].gameObject.GetComponent<MeshFilter>();

            if (meshFilter1.sharedMesh.bounds.center.z > meshFilter2.sharedMesh.bounds.center.z)
            {
                Positive = meshFilter1.gameObject;
                Negative = meshFilter2.gameObject;
            }
            else
            {
                Positive = meshFilter2.gameObject;
                Negative = meshFilter1.gameObject;
            }
        }
    }
}