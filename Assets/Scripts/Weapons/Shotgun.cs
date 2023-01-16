using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Weapons
{
    public class Shotgun : PlayerWeapon
    {
        public override void Shoot()
        {
            for (int i = 0; i < 3; i++)
            {
                var proj = _pool.GetObject();
                proj.InitializeProjectile(invertedDirectionForProjectile);
                proj.gameObject.SetActive(true);
                proj.transform.position = new Vector3(transform.position.x, transform.position.y+proj.SpriteHeight, 
                    transform.position.z);
                proj.transform.rotation = Quaternion.identity;
                if (i == 0)
                {
                    proj.transform.position = new Vector3(transform.position.x-0.5f, transform.position.y+proj.SpriteHeight, 
                        transform.position.z);
                    proj.transform.Rotate(Vector3.forward, 25f,Space.World);
                }else if (i == 2)
                {
                    proj.transform.position = new Vector3(transform.position.x+0.5f, transform.position.y+proj.SpriteHeight, 
                        transform.position.z);
                    proj.transform.Rotate(Vector3.forward, -25f,Space.World);
                }
            }
        }
    }
}