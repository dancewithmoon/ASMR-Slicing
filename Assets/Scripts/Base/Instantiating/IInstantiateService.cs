using Base.Services;
using UnityEngine;

namespace Base.Instantiating
{
    public interface IInstantiateService : IService
    {
        GameObject Instantiate(GameObject prefab);
        GameObject Instantiate(GameObject prefab, Vector3 at);
        GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent);
        GameObject Instantiate(GameObject prefab, Transform parent);
        GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation);
    }
}