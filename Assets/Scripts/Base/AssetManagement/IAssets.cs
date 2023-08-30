using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Services;
using UnityEngine;

namespace Base.AssetManagement
{
    public interface IAssets : IService
    {
        Task<T> Load<T>(string path) where T : Object;
        Task<IEnumerable<T>> LoadAll<T>(string path) where T : Object;
        void CleanUp();
    }
}