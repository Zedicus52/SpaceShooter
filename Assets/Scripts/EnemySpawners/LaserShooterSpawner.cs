using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.EnemySpawners
{
    public class LaserShooterSpawner : EnemySpawner
    {
        protected override void InitializeEnemy()
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
            enemy.InitializeEnemy(enemy.transform);
            enemy.EnemyDestroyed += OnEnemyDestroy;
            float x = Random.Range(enemy.Border.LeftSide, enemy.Border.RightSide);
            float y = Random.Range(-0.5f, 0.5f);
            enemy.transform.position = new Vector3(x,y,0);
            enemy.gameObject.SetActive(true);
            _currentCount += 1;
        }
        
        protected override IEnumerator Spawn()
        {
            while (_canSpawn)
            {
                InitializeEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}