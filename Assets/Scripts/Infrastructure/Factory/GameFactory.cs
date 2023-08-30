using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Data;
using Base.Instantiating;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets, IInstantiateService instantiateService) : base(assets, instantiateService)
        {
            _assets = assets;
        }

        public override async Task Preload()
        {
            await Task.Delay(50);
        }

        public void CreateKnife(GameObjectSceneData gameObjectData)
        {
            InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);
        }

        public void CreateSliceableItem(GameObjectSceneData gameObjectData)
        {
            InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);
        }
    }
}