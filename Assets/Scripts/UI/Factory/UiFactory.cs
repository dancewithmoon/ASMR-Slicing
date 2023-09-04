using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Instantiating;
using UI.Screens;
using UI.ScreenService;
using UI.StaticData;
using UnityEngine;

namespace UI.Factory
{
    public class UiFactory : BaseFactory, IUiFactory
    {
        private const string UIRootPath = "Prefabs/UIRoot";
        
        private readonly IAssets _assets;
        private readonly IInstantiateService _instantiateService;
        private readonly IUiStaticDataService _uiStaticData;
        private Transform _uiRoot;

        public UiFactory(IAssets assets, IInstantiateService instantiateService, IUiStaticDataService uiStaticData) : base(assets, instantiateService)
        {
            _assets = assets;
            _instantiateService = instantiateService;
            _uiStaticData = uiStaticData;
        }

        public override async Task Preload()
        {
            await _assets.Load<GameObject>(UIRootPath); 
        }

        public async void CreateUIRoot()
        {
            GameObject rootPrefab = await _assets.Load<GameObject>(UIRootPath);
            _uiRoot = _instantiateService.Instantiate(rootPrefab).transform;
        }

        public BaseScreen CreateStartScreen() => 
            CreateScreen(ScreenId.StartScreen).GetComponent<BaseScreen>();

        private GameObject CreateScreen(ScreenId screenId) => 
            InstantiateRegistered(_uiStaticData.GetScreen(screenId).gameObject, _uiRoot);
    }
}