using System.Collections;
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
        [SerializeField] protected AudioSource audioSource;
        
        protected int _currentCount;
        protected Pool<Enemy> _pool;
        protected bool _canSpawn;
        protected Task _waitTask;
        private CancellationTokenSource _cancellationTokenSource;


        private void Awake()
        {
            _pool = new Pool<Enemy>(prefab, transform, false, maxCountForSpawn);
            _cancellationTokenSource = new CancellationTokenSource();
            _waitTask = new Task(WaitForCanSpawn, _cancellationTokenSource.Token);
        }
        
        protected abstract IEnumerator Spawn();

        protected virtual void OnEnable()
        {
            _canSpawn = true;
            StartCoroutine(Spawn());
        }

        protected virtual void InitializeEnemy()
        {
            if (_currentCount >= maxCountForSpawn)
            {
                _canSpawn = false;
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                _waitTask = new Task(WaitForCanSpawn, _cancellationTokenSource.Token);
                _waitTask.Start();
                return;
            }
            var enemy = _pool.GetObject();
            enemy.InitializeEnemy(parentObjectForProjectiles);
            enemy.EnemyDestroyed += OnEnemyDestroy;
            float x = Random.Range(enemy.Border.LeftSide, enemy.Border.RightSide);
            float y = Random.Range(enemy.Border.DownSide*0.5f, enemy.Border.UpSide);
            enemy.transform.position = new Vector3(x,y,0);
            enemy.gameObject.SetActive(true);
            _currentCount += 1;
        }
        
        protected virtual void OnEnemyDestroy(Enemy enemy)
        {
            _currentCount -= 1;
            audioSource.PlayOneShot(enemy.DestroyClip);
            enemy.EnemyDestroyed -= OnEnemyDestroy;
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