using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.UI
{
    public class GameRestarter : ButtonAction
    {
        protected override void Action()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ProjectContext.Instance.PauseManager.SetPaused(false);
            Time.timeScale = 1f;
        }
    }
}