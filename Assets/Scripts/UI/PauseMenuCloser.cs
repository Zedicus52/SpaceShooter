using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class PauseMenuCloser : ButtonAction
    {
        [SerializeField] private Transform pausePanel;
        protected override void Action()
        {
            if(ProjectContext.Instance.PauseManager.IsPaused == false)
                return;
            pausePanel.gameObject.SetActive(false);
            ProjectContext.Instance.PauseManager.SetPaused(false);
            Time.timeScale = 1f;
        }
    }
}