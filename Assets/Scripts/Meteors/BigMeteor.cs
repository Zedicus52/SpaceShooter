using System;
using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;
using UnityEngine;

namespace SpaceShooter.Meteors
{
    public class BigMeteor : Meteor
    {
        protected override void OnEnable()
        {
            OnMeteorDestroy += MeteorsSpawner.Instance.OnBigMeteorDestroy;
            base.OnEnable();
        }
           

        protected override void OnDisable()
        {
            OnMeteorDestroy -= MeteorsSpawner.Instance.OnBigMeteorDestroy;
            base.OnDisable();
        }
            

        protected override void MeteorDestroy()
        {
            OnMeteorDestroyInvoker(this);
            gameObject.SetActive(false);
            ProjectContext.Instance.ScoreManager.AddPoints(ScoreInfo.BigMeteorScore);
        }
    }
}