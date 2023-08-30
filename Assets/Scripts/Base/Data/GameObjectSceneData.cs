using System;
using UnityEngine;

namespace Base.Data
{
    [Serializable]
    public class GameObjectSceneData
    {
        [SerializeField] private TransformSceneData _transformData;
        [SerializeField] private GameObject _prefab;

        public TransformSceneData TransformData => _transformData;
        public GameObject Prefab => _prefab;
    }
}