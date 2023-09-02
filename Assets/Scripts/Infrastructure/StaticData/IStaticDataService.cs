using Base.Services;
using Base.States;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService : IService, IPreloadedInBootstrap
    {
        LevelStaticData GetLevelStaticData();
        KnifeStaticData GetKnifeStaticData();
    }
}