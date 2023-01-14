using System;
using System.Collections;
using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;
using SpaceShooter.Weapons;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    [RequireComponent(typeof(BombardierWeapon))]
    public class Bombardier : Enemy
    {
        [SerializeField] private float attackDelay;
        private bool _isMoveRight;
        private bool _isStartAttack = true;
        private bool _isEndAttack = false;
        private Transform _playerTransform;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float _currentTime;
        private bool _timerIsWork;

        protected override void Awake()
        {
            _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
            base.Awake();
        }

        public override void OnEnable()
        {
            _timerIsWork = false;
            _isStartAttack = true;
            SetDirection();
            _currentHealth = maxHealth;
            _canMove = true;
            _currentTime = attackDelay;
        }

        public override void Update()
        {
            if(_canMove == false)
                return;

            if (_timerIsWork)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    _timerIsWork = false;
                    _isEndAttack = false;
                    _isStartAttack = true;
                    _currentTime = attackDelay;
                }
            }


            if (_isStartAttack)
                MoveToPlayer();
            else if (_isEndAttack)
                MoveFromPlayerPlayer();
            else if(_isMoveRight)
                MoveRight();
            else if(_isMoveRight == false)
                MoveLeft();
               
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
        
        private void MoveToPlayer()
        {
            if (_startPosition == Vector3.zero)
                _startPosition = _transform.position;

            _endPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y + SpriteSize.y*2f,
                _playerTransform.position.z);
            
            _transform.position = Vector3.MoveTowards(_transform.position, _endPosition,
                speed * Time.deltaTime);
            if (_transform.position == _endPosition)
            {
                StartCoroutine(Shoot());
                _isEndAttack = true;
                _isStartAttack = false;
            }
                
        }
        private void MoveFromPlayerPlayer()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _startPosition, 
                speed * Time.deltaTime);
            if (_transform.position == _startPosition)
            {
                _startPosition = Vector3.zero;
                _isEndAttack = false;
            }
        }
        
        private void SetDirection()
        {
            float distanceToLeftSide = Vector3.Distance(_transform.position,
                new Vector3(ScreenBounds.LeftSide, _transform.position.y, _transform.position.z));
            float distanceToRightSide = Vector3.Distance(_transform.position,
                new Vector3(ScreenBounds.RightSide, _transform.position.y, _transform.position.z));

            _isMoveRight = Math.Abs(Mathf.Max(distanceToRightSide, distanceToLeftSide) - distanceToRightSide) < 0.1f;
        }

        protected override IEnumerator Shoot()
        {
            _weapon.Shoot();
            StartTimer();
            yield break;
        }

        private void StartTimer() => _timerIsWork = true;
       
    }
}