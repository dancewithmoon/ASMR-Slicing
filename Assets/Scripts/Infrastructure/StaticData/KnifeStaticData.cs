using System;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "KnifeStaticData", menuName = "StaticData/Knife")]
    public class KnifeStaticData : ScriptableObject
    {
        [SerializeField] private KnifeParameters _knifeParameters;

        public KnifeParameters KnifeParameters => new KnifeParameters(_knifeParameters);
    }

    [Serializable]
    public class KnifeParameters
    {
        [SerializeField] private Vector3 _sliceDirection;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _releaseDuration;

        public KnifeParameters(KnifeParameters data)
        {
            _sliceDirection = data.SliceDirection;
            _moveDirection = data.MoveDirection;
            _moveSpeed = data.MoveSpeed;
            _releaseDuration = data.ReleaseDuration;
        }

        public Vector3 SliceDirection => _sliceDirection;
        public Vector3 MoveDirection => _moveDirection;
        public float MoveSpeed => _moveSpeed;
        public float ReleaseDuration => _releaseDuration;
    }
}