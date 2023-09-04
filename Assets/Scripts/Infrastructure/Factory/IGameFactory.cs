using Base.Data;
using Base.Services;
using Base.States;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService, IPreloadedInLoadLevel, ICleanUp
    {
        GameObject Knife { get; }
        GameObject SliceableItem { get; set; }
        
        void CreateKnife(GameObjectSceneData gameObjectData);
        void CreateSliceableItem(GameObjectSceneData levelDataItemData);
    }
}