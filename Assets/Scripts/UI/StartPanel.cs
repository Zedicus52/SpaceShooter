using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShooter.UI
{
    public class StartPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private List<GameObject> objectsToEnableWithDelay;
        [SerializeField] private List<GameObject> objectsToEnableWithoutDelay;
        [SerializeField] private TMP_Text startText;
        private bool _canAnimate = true;

        private void Awake()
        {
            Time.timeScale = 0.0f;
            TextAnimation();
        }

        private async void TextAnimation()
        {
            bool addAlpha = false;
            while (_canAnimate)
            {
                if (addAlpha)
                    startText.alpha += 0.05f;
                if (!addAlpha)
                    startText.alpha -= 0.05f;
                if (startText.alpha <= 0.3f)
                {
                    addAlpha = true;
                }else if (startText.alpha >= 1.0f)
                {
                    addAlpha = false;
                }

                await Task.Delay(50);
            }
        }

        public async void OnPointerClick(PointerEventData eventData)
        {
            _canAnimate = false;
            startText.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            await EnableWithDelay();
            EnableWithoutDelay();
            Destroy(gameObject);
        }

        private async Task EnableWithDelay()
        {
            foreach (var obj in objectsToEnableWithDelay)
            {
                await Task.Delay(100);
                obj.SetActive(true);
            }
        }
        private void EnableWithoutDelay()
        {
            foreach (var obj in objectsToEnableWithoutDelay)
            {
                obj.SetActive(true);
            }
        }
    }
}
