using System;
using System.Collections;
using SpaceShooter.Abstraction;
using SpaceShooter.DataStructures;
using SpaceShooter.Weapons;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    [RequireComponent(typeof(HealerWeapon))]
    public class Healer : Enemy
    {
        [SerializeField] private int healAmount;
        [SerializeField] private float healDelay;
        [SerializeField] private float lifeTime;
        private Enemy _enemyForHeal;
        private bool _isMoveRight;
        private float _currentTime;
        private bool _timerIsWork;

        protected override void Awake()
        {
            Score = ScoreInfo.HealerScore;
            base.Awake();
        }
        
        public override void OnEnable()
        {
           base.OnEnable();
           SetDirection();
           _currentTime = lifeTime;
           _timerIsWork = true;
           _enemyForHeal = null;
        }

        protected override void OnDisable()
        {
            _timerIsWork = false;
            base.OnDisable();
        }

        public override void Update()
        {
            if(_canMove == false)
                return;

            if (_timerIsWork)
            {
               TimerWork();
            }
            
            if(_isMoveRight)
                MoveRight();
            else
                MoveLeft();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Enemy enemy))
            {
                _enemyForHeal = enemy;
                StartCoroutine(Heal());
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out Enemy enemy))
            {
                if (enemy == _enemyForHeal)
                    _enemyForHeal = null;
            }
        }

        private IEnumerator Heal()
        {
            if(_enemyForHeal == null)
                yield break;
            while (_enemyForHeal != null)
            {
                _enemyForHeal.RestoreHealth(healAmount);
                yield return new WaitForSeconds(healDelay);
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

        private void TimerWork()
        {
            if (_currentTime <= 0)
            {
                _timerIsWork = false;
                gameObject.SetActive(false);
                return;
            }

            _currentTime -= Time.deltaTime;
        }
    }
}