using Base.Data;
using Base.Services;
using Base.States;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService, IPreloadedInLoadLevel, ICleanUp
    {
        void CreateKnife(GameObjectSceneData gameObjectData);
        void CreateSliceableItem(GameObjectSceneData levelDataItemData);
    }
}