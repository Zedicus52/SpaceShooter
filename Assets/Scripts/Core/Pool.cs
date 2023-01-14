using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceShooter.Core
{
    public class Pool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private int _basicCount;
        private bool _isAutoExpand;
        private Transform _parentObject;
        private List<T> _objects;

        public Pool(T prefab, Transform parentObject, bool isAutoExpand, int basicCount = 20)
        {
            _prefab = prefab;
            _parentObject = parentObject;
            _isAutoExpand = isAutoExpand;
            _basicCount = basicCount;
            InitializePool();
        }

        public T GetObject()
        {
            if (HasFree() == false && _isAutoExpand == false)
                throw new NullReferenceException($"Cannot get object of type {typeof(T)}");


            foreach (var obj in _objects)
            {
                if (obj.gameObject.activeInHierarchy == false)
                    return obj;
            }
            var newObj = CreateObject(true);
            _objects.Add(newObj);
            return newObj;
        }
        private void InitializePool()
        {
            _objects = new List<T>();
            for (int i = 0; i < _basicCount; i++)
            {
                _objects.Add(CreateObject());
            }
        }

        private T CreateObject(bool isVisibly = false)
        {
            var obj = Object.Instantiate(_prefab, _parentObject);
            obj.gameObject.SetActive(isVisibly);
            return obj;
        }
        
        private bool HasFree()
        {
            foreach (var obj in _objects)
            {
                if (obj.gameObject.activeInHierarchy == false)
                    return true;
            }

            return false;
        }
    }
}
