using System;
using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.Weapons;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    [RequireComponent(typeof(KamikazeWeapon))]
    public class Kamikaze : Enemy
    {
        private Transform _playerTransform;
        private Vector3 _movementPoint;
        protected override void Awake()
        {
            _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
            base.Awake();
        }

        public override void OnEnable()
        {
            _currentHealth = maxHealth;
            _canMove = true;
            _movementPoint = _playerTransform.position;
            RotateToTarget();
        }

        protected override void OnDisable()
        {
            _transform.rotation = Quaternion.identity;
            _canMove = false;
        }

        public override void Update()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _movementPoint, 
                speed * Time.deltaTime);
            if(_transform.position == _movementPoint && gameObject.activeInHierarchy)
                gameObject.SetActive(false);
        }

        private void RotateToTarget()
        {
            Vector3 newRotationDirection = Vector3.RotateTowards(_transform.position, _movementPoint, 
                speed * Time.deltaTime, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(newRotationDirection);
            _transform.rotation = Quaternion.Slerp( new Quaternion(_transform.rotation.x, _transform.rotation.y, -newRotation.z,
                _transform.rotation.w),_transform.rotation, 0.7f);
        }
    }
}