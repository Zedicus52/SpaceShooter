using System;
using System.Collections;
using SpaceShooter.Abstraction;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceShooter.Core
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerShooter : MonoBehaviour
    {
        public event Action<PlayerWeapon> WeaponChanged;
        public event Action<float> WeaponShootDelayChanged;
        public PlayerWeapon CurrentWeapon => currentWeapon;
        [SerializeField] private Transform parentForProjectiles;
        [SerializeField] private PlayerWeapon currentWeapon;
        [FormerlySerializedAs("source")] [SerializeField] private AudioSource audioSource;
        private bool _canShoot;
        private void OnEnable()
        {
            _canShoot = true;
            currentWeapon.InitializeWeapon(parentForProjectiles);
            StartCoroutine(ShootCycle());
        }

        private IEnumerator ShootCycle()
        {
            while (_canShoot)
            {
                currentWeapon.Shoot();
                audioSource.PlayOneShot(currentWeapon.ShootSound);
                yield return new WaitForSeconds(currentWeapon.ShootDelay);
            }
        }

        public void OnWeaponChanged(PlayerWeapon weapon) => WeaponChanged?.Invoke(weapon);
        public void OnWeaponShootDelayChanged(float percent) => WeaponShootDelayChanged?.Invoke(percent);

        public void SetWeapon(PlayerWeapon weapon)
        {
            _canShoot = false;
            currentWeapon = weapon;
            currentWeapon.InitializeWeapon(parentForProjectiles);
            _canShoot = true;
        }
    }
}
 