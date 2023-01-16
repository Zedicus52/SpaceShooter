using System;
using System.Collections.Generic;
using System.Linq;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class PlayerWeaponContainer : MonoBehaviour
    {
        [SerializeField] private PlayerShooter playerShooter;
        [SerializeField] private List<PlayerWeapon> weapons;

        private void OnEnable()
        {
            playerShooter.WeaponChanged += PlayerShooterOnWeaponChanged;
            playerShooter.WeaponShootDelayChanged += PlayerShooterOnWeaponShootDelayChanged;
        }

      

        private void OnDisable()
        {
            playerShooter.WeaponChanged -= PlayerShooterOnWeaponChanged;
            playerShooter.WeaponShootDelayChanged -= PlayerShooterOnWeaponShootDelayChanged;
        }
        private void PlayerShooterOnWeaponShootDelayChanged(float obj)
        {
            foreach (var weapon in weapons)
            {
                weapon.DecreaseShootDelay(obj);
            }
        }
        private void PlayerShooterOnWeaponChanged(PlayerWeapon obj)
        {
            var weapon = weapons.FirstOrDefault(x => x == obj);
            weapon.gameObject.SetActive(true);
            playerShooter.SetWeapon(weapon);
        }
    }
}