using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Weapons
{
    public class BombardierWeapon : Weapon
    {
        public override void Shoot()
        {
            var proj = _pool.GetObject();
            proj.InitializeProjectile(invertedDirectionForProjectile);
            proj.transform.position = new Vector3(transform.position.x, transform.position.y-proj.SpriteHeight*1.8f, 
                transform.position.z);
            proj.gameObject.SetActive(true);
        }
    }
}