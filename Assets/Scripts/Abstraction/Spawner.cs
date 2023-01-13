using UnityEngine;

namespace SpaceShooter.Abstraction
{
    public abstract class Spawner<T> where T :  MonoBehaviour
    {
        [SerializeField] protected T _prefab;
        [SerializeField] protected int _basicCount;
        [SerializeField] protected bool _isAutoExpand;
        
        protected abstract void Spawn();
    }
}