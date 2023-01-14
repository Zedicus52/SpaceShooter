using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.Abstraction
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [SerializeField] protected int maxCountForSpawn;
        [SerializeField] protected float spawnDelay;
        [SerializeField] protected Enemy prefab;
        [SerializeField] protected Transform parentObjectForProjectiles;
        
        protected int _currentCount;
        protected Pool<Enemy> _pool;
        protected List<Enemy> _enabledEnemies;
        protected bool _canSpawn;
        protected Task _waitTask;
        protected CancellationTokenSource _cancellationTokenSource;


        private void Awake()
        {
            _pool = new Pool<Enemy>(prefab, transform, false, maxCountForSpawn);
            _enabledEnemies = new List<Enemy>();
            _cancellationTokenSource = new CancellationTokenSource();
            _waitTask = new Task(WaitForCanSpawn, _cancellationTokenSource.Token);
        }
        
        protected abstract IEnumerator Spawn();

        protected virtual void OnEnable()
        {
            _canSpawn = true;
            StartCoroutine(Spawn());
        }

        protected virtual void OnDisable()
        {
            _enabledEnemies.Clear();
        }
        protected virtual void InitializeEnemy()
        {
            if (_currentCount >= maxCountForSpawn)
            {
                _canSpawn = false;
                _waitTask.Start();
                return;
            }
            var enemy = _pool.GetObject();
            enemy.InitializeEnemy(parentObjectForProjectiles);
            enemy.EnemyDestroyed += OnEnemyDestroy;
            float x = Random.Range(enemy.Border.LeftSide, enemy.Border.RightSide);
            float y = Random.Range(0.1f, enemy.Border.UpSide);
            enemy.transform.position = new Vector3(x,y,0);
            enemy.gameObject.SetActive(true);
            _currentCount += 1;
        }
        
        protected virtual void OnEnemyDestroy(Enemy enemy)
        {
            _currentCount -= 1;
            enemy.EnemyDestroyed -= OnEnemyDestroy;
            _enabledEnemies.Remove(enemy);
        }

        private async void WaitForCanSpawn()
        {
            while (_currentCount >= maxCountForSpawn)
                await Task.Delay(50);
            _canSpawn = true;
            StartCoroutine(Spawn());
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            _waitTask = new Task(WaitForCanSpawn, _cancellationTokenSource.Token);
        }
    }
}