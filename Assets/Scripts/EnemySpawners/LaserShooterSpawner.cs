using System.Collections;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.EnemySpawners
{
    public class LaserShooterSpawner : EnemySpawner
    {
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