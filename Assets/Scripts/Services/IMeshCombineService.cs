using Base.Services;
using UnityEngine;

namespace Services
{
    public interface IMeshCombineService : IService
    {
        void Combine(GameObject gameObject, bool useMeshCollider = false);
    }
}