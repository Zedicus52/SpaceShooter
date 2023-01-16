using SpaceShooter.Abstraction;
using SpaceShooter.DataStructures;
using UnityEngine;

namespace SpaceShooter.Projectiles
{
    public class ShotgunProjectile : Projectile
    {
        protected override void Update()
        {
            if(IsPaused)
                return;
            
            if (_transform.position.y >= ScreenBounds.UpSide || _transform.position.y <= ScreenBounds.DownSide 
                || _transform.position.x >= ScreenBounds.RightSide || _transform.position.x <= ScreenBounds.LeftSide)
            { 
                gameObject.SetActive(false);
                return;
            }

            float newX = _transform.position.x;
            if (_transform.rotation.z > 0)
            {
                newX -= speed*0.5f * Time.deltaTime;
            }
            else if (_transform.rotation.z < 0)
            {
                newX += speed*0.5f * Time.deltaTime;
            }

            float newY = _transform.position.y + speed * Time.deltaTime;
            
            _transform.position = new Vector3(newX, newY, _transform.position.z);
        }
    }
}