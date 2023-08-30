using UnityEngine;

namespace Base.Instantiating
{
    public class InstantiateService : IInstantiateService
    {
        public GameObject Instantiate(GameObject prefab) => 
            Object.Instantiate(prefab);

        public GameObject Instantiate(GameObject prefab, Vector3 at) => 
            Object.Instantiate(prefab, at, Quaternion.identity);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent) => 
            Object.Instantiate(prefab, at, Quaternion.identity, parent);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation) =>
            Object.Instantiate(prefab, at, rotation);

        public GameObject Instantiate(GameObject prefab, Transform parent) => 
            Object.Instantiate(prefab, parent);
    }
}