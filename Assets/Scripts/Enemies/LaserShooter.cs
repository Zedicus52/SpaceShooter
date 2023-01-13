using System;
using System.Collections;
using System.Collections.Generic;
using SpaceShooter.Abstraction;
using SpaceShooter.DataStructures;
using SpaceShooter.Projectiles;
using SpaceShooter.Weapons;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    [RequireComponent(typeof(LaserShooterWeapon))]
    public class LaserShooter : Enemy
    {
        private bool _isMoveRight;
        
        public override void OnEnable()
        {
            SetDirection();
            base.OnEnable();
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
                _canMove = false;
                _weapon.Shoot();
                if (_weapon.Projectile is LaserProjectile proj)
                {
                    yield return new WaitForSeconds(proj.AwaitTime);
                    _canMove = true;
                }
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