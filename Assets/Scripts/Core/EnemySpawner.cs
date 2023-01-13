using System.Collections;
using SpaceShooter.Abstraction;
using SpaceShooter.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Core
{
    public class EnemySpawner : MonoBehaviour, IPauseHandler {
        public static EnemySpawner Instance { get; private set; }
        
        [Header("Main settings")] 
        [SerializeField] private Transform projectileParentObject;
        
        [Header("Gunner")]
        [SerializeField] private Gunner gunner;
        [SerializeField] private float gunnerSpawnDelay;
        [SerializeField] private int gunnerMaxCount;
        [Header("Kamikaze")]
        [SerializeField] private Kamikaze kamikaze;
        [SerializeField] private float kamikazeSpawnDelay;
        [SerializeField] private int kamikazeMaxCount;
        [Header("Healer")]
        [SerializeField] private Healer healer;
        [SerializeField] private float healerSpawnDelay;
        [SerializeField] private int healerMaxCount;
        [Header("Bombardier")]
        [SerializeField] private Bombardier bombardier;
        [SerializeField] private float bombardierSpawnDelay;
        [SerializeField] private int bombardierMaxCount;
        [Header("Laser Shooter")]
        [SerializeField] private LaserShooter laserShooter;
        [SerializeField] private float laserShooterSpawnDelay;
        [SerializeField] private int laserShooterMaxCount;

        private Pool<Gunner> _gunnersPool;
        private Pool<Kamikaze> _kamikazesPool;
        private Pool<Healer> _healersPool;
        private Pool<Bombardier> _bombardiersPool;
        private Pool<LaserShooter> _laserShootersPool;

        private IEnumerator _gunnerSpawnCoroutine;
        private IEnumerator _kamikazeSpawnCoroutine;
        private IEnumerator _healerSpawnCoroutine;
        private IEnumerator _bombardierSpawnCoroutine;
        private IEnumerator _laserShooterSpawnCoroutine;
        
        private int _gunnerCount;
        private int _kamikazeCount;
        private int _healerCount;
        private int _bombardierCount;
        private int _laserShooterCount;

        private bool _canSpawn;
        private bool _isPaused;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                ProjectContext.Instance.PauseManager.Register(this);
                _canSpawn = true;
                InitializePools();
                SetCoroutines();
                StartCoroutine(_gunnerSpawnCoroutine);
                _gunnerCount = 0;
                return;
            }
            Destroy(gameObject);
        }

        private void InitializePools()
        {
            _gunnersPool = new Pool<Gunner>(gunner, transform, false, gunnerMaxCount+1);
        }
        
        private void SetCoroutines()
        {
            _gunnerSpawnCoroutine = GunnerSpawnCycle();
        }



        public void SetPaused(bool isPaused) => _isPaused = isPaused;
       

        private IEnumerator GunnerSpawnCycle()
        {
            while (_canSpawn)
            {
                if (_gunnerCount + 1 >= gunnerMaxCount)
                {
                    _canSpawn = false;
                    StopCoroutine(_gunnerSpawnCoroutine);
                    break;
                }
                SpawnGunner();
                yield return new WaitForSeconds(gunnerSpawnDelay);
            }
        }

        private void SpawnGunner()
        {
            if(_isPaused)
                return;
            
            var enemy  = _gunnersPool.GetObject();
            enemy.InitializeEnemy(projectileParentObject);
            float x = Random.Range(enemy.Border.LeftSide, enemy.Border.RightSide);
            float y = Random.Range(enemy.Border.DownSide * 0.5f, enemy.Border.UpSide);
            enemy.transform.position = new Vector3(x,y,0);
            enemy.gameObject.SetActive(true);
            _gunnerCount += 1;
        }

        private void GunnerDestroy()
        {
            _gunnerCount -= 1;
            if (_canSpawn == false)
            {
                _canSpawn = true;
                StartCoroutine(GunnerSpawnCycle());
            }
        }
    }
}