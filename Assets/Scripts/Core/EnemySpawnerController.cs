using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    [RequireComponent(typeof(EnemySpawner))]
    public class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField] private float delayBeforeSpawningNewEnemyType;
        private List<EnemySpawner> _spawners;

        private void Awake()
        {
            _spawners = GetComponents<EnemySpawner>().ToList();
            foreach (var spawner in _spawners)
            {
                spawner.enabled = false;
            }

            StartCoroutine(SpawnerInitialization());
        }

        private IEnumerator SpawnerInitialization()
        {
            foreach (var spawner in _spawners)
            {
                yield return new WaitForSeconds(3);
                spawner.enabled = true;
                yield return new WaitForSeconds(delayBeforeSpawningNewEnemyType);
            }
        }
    }
}