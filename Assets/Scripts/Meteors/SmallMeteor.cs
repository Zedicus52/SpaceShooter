using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;

namespace SpaceShooter.Meteors
{
    public class SmallMeteor : Meteor
    {
        protected override void OnEnable()
        {
            OnMeteorDestroy += MeteorsSpawner.Instance.OnSmallMeteorDestroy;
            base.OnEnable();
        }
           

        protected override void OnDisable()
        {
            OnMeteorDestroy -= MeteorsSpawner.Instance.OnSmallMeteorDestroy;
            base.OnDisable();
        }
        protected override void MeteorDestroy() 
        {
            OnMeteorDestroyInvoker(this);
            gameObject.SetActive(false);
            ProjectContext.Instance.ScoreManager.AddPoints(ScoreInfo.SmallMeteorScore);
        }
    }
}