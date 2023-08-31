using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Data;
using Base.Instantiating;
using Logic;
using Services;
using UnityEngine;
using UserInput;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IInputService _inputService;
        private readonly IMeshCombineService _meshCombineService;
        
        public GameObject Knife { get; private set; }
        public GameObject SliceableItem { get; private set; }
        
        public GameFactory(IAssets assets, IInstantiateService instantiateService, IInputService inputService, IMeshCombineService meshCombineService) : base(assets, instantiateService)
        {
            _assets = assets;
            _inputService = inputService;
            _meshCombineService = meshCombineService;
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
            Knife = instance;
        }

        public void CreateSliceableItem(GameObjectSceneData gameObjectData)
        {
            GameObject instance = InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);

            _meshCombineService.Combine(instance, true);
            SliceableItem = instance;
        }
    }
}