using Base.Data;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [Header("Knife")]
        [SerializeField] private GameObjectSceneData _knifeSceneData;
        [SerializeField] private Vector3 _knifeRemovedPosition;
        
        [Header("Slice")]
        [SerializeField] private GameObjectSceneData _sliceSceneData;
        [SerializeField] private Vector3 _sliceFinalPosition;

        public GameObjectSceneData KnifeSceneData => _knifeSceneData;
        public Vector3 KnifeRemovedPosition => _knifeRemovedPosition;
        
        public GameObjectSceneData SliceSceneData => _sliceSceneData;
        public Vector3 SliceFinalPosition => _sliceFinalPosition;
    }
}