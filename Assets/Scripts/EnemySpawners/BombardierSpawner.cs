using System.Collections;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.EnemySpawners
{
    public class BombardierSpawner : EnemySpawner
    {
        protected override IEnumerator Spawn()
        {
            while (_canSpawn)
            {
                InitializeEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        protected override void InitializeEnemy()
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
            float y = Random.Range(0.1f, enemy.Border.UpSide-enemy.SpriteSize.y*0.5f);
            enemy.transform.position = new Vector3(x,y,0);
            enemy.gameObject.SetActive(true);
            _currentCount += 1;
        }
    }
}