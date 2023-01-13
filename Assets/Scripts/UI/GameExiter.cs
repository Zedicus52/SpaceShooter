using System.Net.Mime;
using SpaceShooter.Abstraction;
using UnityEngine.Device;


namespace SpaceShooter.UI
{
    public class GameExiter : ButtonAction
    {
        protected override void Action()
        {
            Application.Quit(0);
        }
    }
}