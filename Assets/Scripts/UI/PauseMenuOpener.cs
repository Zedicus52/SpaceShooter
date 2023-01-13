using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class PauseMenuOpener : ButtonAction
    {
        [SerializeField] private Transform pausePanel;
        protected override void Action()
        {
            if(ProjectContext.Instance.PauseManager.IsPaused)
                return;
            pausePanel.gameObject.SetActive(true);
            ProjectContext.Instance.PauseManager.SetPaused(true);
            Time.timeScale = 0f;
        }
    }
}