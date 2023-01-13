using System;
using System.Collections.Generic;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Weapons
{
    public class KamikazeWeapon : Weapon
    {
        public override void Shoot()
        {
            Debug.Log("Kamikaze weapon cannot shoot!");
        }
    }
}