using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SpaceShooter.Core
{
    public class LocalAssetLoader
    {
        private GameObject _cachedObject;

        public async Task<T> LoadInternal<T>(string assetId)
        {
            var handle = Addressables.InstantiateAsync(assetId);
            _cachedObject = await handle.Task;
            if (_cachedObject.TryGetComponent(out T obj) == false)
                throw new NullReferenceException($"Object of type {typeof(T)} is null on attempt to load it");
            return obj;
        }

        public async void UnloadInternal()
        {
            if(_cachedObject == null)
                return;
            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}