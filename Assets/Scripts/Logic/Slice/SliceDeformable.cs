﻿using System.Collections.Generic;
using System.Linq;
using Deform;
using UnityEngine;

namespace Logic.Slice
{
    [RequireComponent(typeof(MeshFilter))]
    public class SliceDeformable : MonoBehaviour
    {
        [SerializeField] private float _angleRatio = 80;
        [SerializeField] private float _widthToRemove = 1;
        [SerializeField] private float _degreesToRemove = 100;
        [SerializeField] private float _minAngle = 10;
        [SerializeField] private float _maxAngle = 180;
        private MeshFilter _meshFilter;
        private List<Deformable> _deformables;
        
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _deformables = GetComponentsInChildren<Deformable>().ToList();
        }

        public void SetDeformer(BendDeformer deformer)
        {
            float angle = _angleRatio / (_meshFilter.sharedMesh.bounds.size.z - _widthToRemove) - _degreesToRemove;
            deformer.Angle = Mathf.Clamp(angle, _minAngle, _maxAngle);
            _deformables.ForEach(deformable => deformable.AddDeformer(deformer));
        }
    }
}