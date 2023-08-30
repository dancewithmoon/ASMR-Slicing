using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Data;
using Base.Instantiating;
using Logic;
using UnityEngine;
using UserInput;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IInputService _inputService;

        public GameFactory(IAssets assets, IInstantiateService instantiateService, IInputService inputService) : base(assets, instantiateService)
        {
            _assets = assets;
            _inputService = inputService;
        }

        public override async Task Preload()
        {
            await Task.Delay(50);
        }

        public void CreateKnife(GameObjectSceneData gameObjectData)
        {
            GameObject instance = InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);
            
            instance.GetComponent<KnifeController>().Initialize(_inputService);
        }

        public void CreateSliceableItem(GameObjectSceneData gameObjectData)
        {
            InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);
        }
    }
}