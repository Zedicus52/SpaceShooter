using System.Collections;
using SpaceShooter.Abstraction;
using SpaceShooter.DataStructures;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Core
{
    public class MeteorsSpawner : MonoBehaviour, IPauseHandler
    {
        public static MeteorsSpawner Instance { get; private set; }
        public bool IsPaused { get; private set; }
        [SerializeField] private float spawnDelay;

        [SerializeField] private float meteorsMaxForce;
        [SerializeField] private float meteorsMinForce;

        [SerializeField] private Meteor bigMeteor;
        [SerializeField] private Meteor mediumMeteor;
        [SerializeField] private Meteor smallMeteor;

        [SerializeField] private Transform meteorsParent;
        [SerializeField] private int basicCount;

        private Pool<Meteor> _bigMeteorsPool;
        private Pool<Meteor> _mediumMeteorsPool;
        private Pool<Meteor> _smallMeteorsPool;

        private BorderFloat _border;
        private bool _canSpawn;
        private IEnumerator _spawnCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                ProjectContext.Instance.PauseManager.Register(this);
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            IsPaused = false;
            _bigMeteorsPool = new Pool<Meteor>(bigMeteor, meteorsParent, true, basicCount);
            _mediumMeteorsPool = new Pool<Meteor>(mediumMeteor, meteorsParent, true, basicCount);
            _smallMeteorsPool = new Pool<Meteor>(smallMeteor, meteorsParent, true, basicCount);
            _border = new BorderFloat(ScreenBounds.LeftSide, ScreenBounds.RightSide, ScreenBounds.DownSide,
                ScreenBounds.UpSide);
            StartSpawn();
        }
        

        private void OnDisable() =>  StopSpawn();
      

        private void StopSpawn()
        {
            _canSpawn = false;
            StopCoroutine(_spawnCoroutine);
        }
        private void StartSpawn()
        {
            _spawnCoroutine = Spawn();
            _canSpawn = true;
            StartCoroutine(_spawnCoroutine);
        }
        
        private IEnumerator Spawn()
        {
            Pool<Meteor>[] pools = new[]
            {
                _bigMeteorsPool,
                _mediumMeteorsPool,
                _smallMeteorsPool
            };
            while (_canSpawn)
            {
                var pool = pools[Random.Range(0, 3)];
                SpawnMeteor(pool);
                yield return new WaitForSecondsRealtime(spawnDelay);
            }
        }
        
        private void SpawnMeteor(Pool<Meteor> pool)
        {
            if(IsPaused)
                return;
            
            var meteor = pool.GetObject();
            meteor.InitializeMeteor(_border);
            float x = Random.Range(meteor.Border.LeftSide, meteor.Border.RightSide);
            float y = meteor.Border.UpSide-0.1f;
            Vector3 position = new Vector3(x, y, 0);
            meteor.transform.position = position;
            meteor.gameObject.SetActive(true);
        }
        
        public void OnBigMeteorDestroy(Vector3 destroyPosition)
        {
            if(IsPaused)
                return;
            for (int i = 0; i < 3; i++)
            {
                Vector3 direction = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 0.2f));
                var meteor = _mediumMeteorsPool.GetObject();
                meteor.InitializeMeteor(_border);
                meteor.transform.position = destroyPosition+direction;
                meteor.gameObject.SetActive(true);
                if (meteor.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.AddForce(direction*Random.Range(meteorsMinForce, meteorsMaxForce),ForceMode2D.Impulse);
                    rb.AddTorque(0.2f, ForceMode2D.Impulse);
                }
            }
        }
        public void OnMediumMeteorDestroy(Vector3 destroyPosition)
        {
            if(IsPaused)
                return;
            for (int i = 0; i < 3; i++)
            {
                Vector3 direction = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 0.2f));
                var meteor = _smallMeteorsPool.GetObject();
                meteor.InitializeMeteor(_border);
                meteor.transform.position = destroyPosition+direction;
                meteor.gameObject.SetActive(true);
                if (meteor.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.AddForce(direction*Random.Range(meteorsMinForce*1.5f, meteorsMaxForce*1.5f),ForceMode2D.Impulse);
                    rb.AddTorque(0.05f, ForceMode2D.Impulse);
                }
            }
        }

        public void SetPaused(bool isPaused) => IsPaused = isPaused;
      
        
        private void OnDestroy() => ProjectContext.Instance.PauseManager.UnRegister(this);
    }
}
