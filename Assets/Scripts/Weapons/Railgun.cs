using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Weapons
{
    public class Railgun : PlayerWeapon
    {
        public override void Shoot()
        {
            var proj = _pool.GetObject();
            proj.InitializeProjectile(invertedDirectionForProjectile);
            proj.transform.position = new Vector3(transform.position.x, transform.position.y+proj.SpriteHeight*1.5f, 
                transform.position.z);
            proj.gameObject.SetActive(true);
        }
    }
}