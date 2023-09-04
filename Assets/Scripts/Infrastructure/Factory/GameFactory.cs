using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Data;
using Base.Instantiating;
using Infrastructure.StaticData;
using Logic;
using UnityEngine;
using UserInput;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticDataService;

        public GameObject Knife { get; private set; }
        public GameObject SliceableItem { get; private set; }
        
        public GameFactory(IAssets assets, IInstantiateService instantiateService, IInputService inputService, IStaticDataService staticDataService) : base(assets, instantiateService)
        {
            _inputService = inputService;
            _staticDataService = staticDataService;
        }

        public override async Task Preload()
        {
            await Task.Delay(50);
        }

        public void CreateKnife(GameObjectSceneData gameObjectData)
        {
            GameObject instance = InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);

            KnifeParameters knifeParameters = _staticDataService.GetKnifeStaticData().KnifeParameters;            
            
            instance.GetComponent<KnifeController>().Initialize(_inputService);
            instance.GetComponent<KnifeMovement>().Initialize(knifeParameters);
            instance.GetComponent<KnifeSlicing>().Initialize(knifeParameters);
            
            Knife = instance;
        }

        public void CreateSliceableItem(GameObjectSceneData gameObjectData)
        {
            GameObject instance = InstantiateRegistered(gameObjectData.Prefab, gameObjectData.TransformData.WorldPosition, 
                Quaternion.Euler(gameObjectData.TransformData.RotationEuler), gameObjectData.TransformData.Scale);

            instance.GetComponent<SliceMovement>().Initialize(_staticDataService.GetLevelStaticData().ItemFinalPosition);
            
            SliceableItem = instance;
        }
    }
}