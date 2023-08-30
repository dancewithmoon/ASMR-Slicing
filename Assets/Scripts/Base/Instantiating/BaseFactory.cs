using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.AssetManagement;
using Base.States;
using UnityEngine;

namespace Base.Instantiating
{
    public abstract class BaseFactory : IPreloadedInLoadLevel, ICleanUp
    {
        private readonly IAssets _assets;
        private readonly IInstantiateService _instantiateService;

        private readonly List<GameObject> _instantiated = new List<GameObject>();
        
        protected BaseFactory(IAssets assets, IInstantiateService instantiateService)
        {
            _assets = assets;
            _instantiateService = instantiateService;
        }

        public abstract Task Preload();
        
        public virtual void CleanUp()
        {
            _instantiated.Where(obj => obj != null).ToList().ForEach(Destroy);
            _instantiated.Clear();
        }

        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, at);
            Register(instance);
            return instance;
        }

        protected GameObject InstantiateRegistered(GameObject prefab, Transform parent)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, parent);
            Register(instance);
            return instance;
        }
        
        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, at, rotation);
            Register(instance);
            return instance;
        }
        
        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at, Quaternion rotation, Vector3 scale)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, at, rotation);
            instance.transform.localScale = scale;
            Register(instance);
            return instance;
        }

        protected async Task<GameObject> InstantiateRegistered(string path)
        {
            GameObject asset = await _assets.Load<GameObject>(path);
            GameObject instance = _instantiateService.Instantiate(asset);
            Register(instance);
            return instance;
        }

        protected async Task<GameObject> InstantiateRegistered(string path, Vector3 at)
        {
            GameObject asset = await _assets.Load<GameObject>(path);
            GameObject instance = _instantiateService.Instantiate(asset, at);
            Register(instance);
            return instance;
        }

        private void Register(GameObject gameObject) => 
            _instantiated.Add(gameObject);
        
        private static void Destroy(GameObject obj) => Object.Destroy(obj);
    }
}