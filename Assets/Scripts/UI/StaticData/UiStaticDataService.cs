using System.Collections.Generic;
using System.Threading.Tasks;
using Base.AssetManagement;
using UI.Screens;
using UI.ScreenService;
using UnityEngine;

namespace UI.StaticData
{
    public class UiStaticDataService : IUiStaticDataService
    {
        private const string ScreensPath = "StaticData/Screens";

        private readonly IAssets _assets;
        
        private Dictionary<ScreenId, BaseScreen> _screens;

        public UiStaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload()
        {
            await Task.WhenAll(
                LoadScreens());
        }

        public BaseScreen GetScreen(ScreenId screenId) =>
            _screens.TryGetValue(screenId, out BaseScreen screen)
                ? screen
                : null;

        private async Task LoadScreens()
        {
            ScreenStaticData screensData = await _assets.Load<ScreenStaticData>(ScreensPath);
            _screens = new Dictionary<ScreenId, BaseScreen>(screensData.Screens);
        }
    }
}