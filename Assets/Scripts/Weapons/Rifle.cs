
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Weapons
{
    public class Rifle : PlayerWeapon
    {
        public override void Shoot()
        {
            var proj = _pool.GetObject();
            proj.InitializeProjectile(invertedDirectionForProjectile);
            proj.transform.position = new Vector3(transform.position.x, transform.position.y+proj.SpriteHeight, 
                transform.position.z);
            proj.gameObject.SetActive(true);
        }
    }
}