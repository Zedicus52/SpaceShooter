using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;

namespace SpaceShooter.Meteors
{
    public class MediumMeteor : Meteor
    {
        protected override void OnEnable()
        {
            OnMeteorDestroy += MeteorsSpawner.Instance.OnMediumMeteorDestroy;
            base.OnEnable();
        }
        
        protected override void OnDisable()
        {
            OnMeteorDestroy -= MeteorsSpawner.Instance.OnMediumMeteorDestroy;
            base.OnDisable();
        }
        
        protected override void MeteorDestroy(bool destroyByPlayer)
        {
            OnMeteorDestroyInvoker(this);
            gameObject.SetActive(false);
            if(destroyByPlayer) 
                ProjectContext.Instance.ScoreManager.AddPoints(ScoreInfo.MediumMeteorScore);
        }

    }
}