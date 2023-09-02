using System.Threading.Tasks;
using Base.AssetManagement;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelStaticDataPath = "LevelStaticData";
        private const string KnifeStaticDataPath = "KnifeStaticData";
        
        private readonly IAssets _assets;

        private LevelStaticData _levelStaticData;
        private KnifeStaticData _knifeStaticData;

        public StaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload()
        {
            await Task.WhenAll(
                LoadLevelStaticData(), 
                LoadKnifeStaticData());
        }

        public LevelStaticData GetLevelStaticData() => _levelStaticData;
        
        public KnifeStaticData GetKnifeStaticData() => _knifeStaticData;

        private async Task LoadLevelStaticData()
        {
            var loaded = await _assets.Load<LevelStaticData>(LevelStaticDataPath);
            _levelStaticData = Object.Instantiate(loaded);
        }

        private async Task LoadKnifeStaticData()
        {
            var loaded = await _assets.Load<KnifeStaticData>(KnifeStaticDataPath);
            _knifeStaticData = Object.Instantiate(loaded);
        }
    }
}