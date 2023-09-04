using Base.Data;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private GameObjectSceneData _knifeData;
        [SerializeField] private GameObjectSceneData _itemData;
        [SerializeField] private Vector3 _itemFinalPosition;

        public GameObjectSceneData KnifeData => _knifeData;
        public GameObjectSceneData ItemData => _itemData;
        public Vector3 ItemFinalPosition => _itemFinalPosition;
    }
}