using System;
using UnityEngine;

namespace Base.Data
{
    [Serializable]
    public class TransformSceneData
    {
        [SerializeField] private Vector3 _worldPosition;
        [SerializeField] private Vector3 _rotationEuler;
        [SerializeField] private Vector3 _scale;

        public Vector3 WorldPosition => _worldPosition;

        public Vector3 RotationEuler => _rotationEuler;

        public Vector3 Scale => _scale;

        public TransformSceneData(Vector3 worldPosition, Vector3 rotationEuler, Vector3 scale)
        {
            _worldPosition = worldPosition;
            _rotationEuler = rotationEuler;
            _scale = scale;
        }
    }
}