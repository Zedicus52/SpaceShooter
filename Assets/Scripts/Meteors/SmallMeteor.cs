using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;

namespace SpaceShooter.Meteors
{
    public class SmallMeteor : Meteor
    {
        protected override void MeteorDestroy() 
        {
            gameObject.SetActive(false);
            ProjectContext.Instance.ScoreManager.AddPoints(ScoreInfo.SmallMeteorScore);
        }
    }
}