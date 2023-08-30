using System.Threading.Tasks;
using Base.AssetManagement;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelStaticDataPath = "LevelStaticData";
        
        private readonly IAssets _assets;

        private LevelStaticData _levelStaticData;

        public StaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload()
        {
            await Task.WhenAll(LoadLevelStaticData());
        }

        public LevelStaticData GetLevelStaticData() => _levelStaticData;

        private async Task LoadLevelStaticData()
        {
            var loaded = await _assets.Load<LevelStaticData>(LevelStaticDataPath);
            _levelStaticData = Object.Instantiate(loaded);
        }
        
    }
}