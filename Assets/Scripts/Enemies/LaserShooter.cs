using System.Collections;
using System.Threading.Tasks;
using SpaceShooter.Abstraction;
using SpaceShooter.DataStructures;
using SpaceShooter.Weapons;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    [RequireComponent(typeof(LaserShooterWeapon))]
    public class LaserShooter : Enemy
    {
        private bool _isMoveRight;
        
        protected override void Awake()
        {
            Score = ScoreInfo.LaserShooterScore;
            base.Awake();
        }
        
        public override async void OnEnable()
        {
           
            SetDirection();
            if(!_isIntialized)
                Initialize(transform);
            _spriteRenderer.color = Color.white;
            _currentHealth = maxHealth;
            _canMove = true;
            _enemyCanShoot = false;
            await Task.Delay(100);
            _enemyCanShoot = true;
            if(_weapon != null && gameObject.activeInHierarchy)
                StartCoroutine(_shootCoroutine);
        }
        
        public override void Update()
        {
            if(_canMove == false)
                return;
            
            if(_isMoveRight)
                MoveRight();
            else
                MoveLeft();
        }

        protected override IEnumerator Shoot()
        {
            while(_enemyCanShoot)
            {
                _weapon.Shoot();
                yield return new WaitForSeconds(_weapon.ShootDelay);
            }
        }

        private void MoveRight()
        {
            Vector3 newPosition = _transform.position;
            newPosition.x += speed * Time.deltaTime;
            if (newPosition.x >= Border.RightSide)
            {
                _isMoveRight = false;
                return;
            }
            _transform.position = newPosition;
        }

        private void MoveLeft()
        {
            Vector3 newPosition = _transform.position;
            newPosition.x -= speed * Time.deltaTime;
            if (newPosition.x <= Border.LeftSide)
            {
                _isMoveRight = true;
                return;
            }
            _transform.position = newPosition;
        }
        
        private void SetDirection()
        {
            float distanceToLeftSide = Vector3.Distance(_transform.position,
                new Vector3(ScreenBounds.LeftSide, _transform.position.y, _transform.position.z));
            float distanceToRightSide = Vector3.Distance(_transform.position,
                new Vector3(ScreenBounds.RightSide, _transform.position.y, _transform.position.z));

            _isMoveRight = Mathf.Abs(Mathf.Max(distanceToRightSide, distanceToLeftSide) - distanceToRightSide) < 0.1f;
        }
    }
}