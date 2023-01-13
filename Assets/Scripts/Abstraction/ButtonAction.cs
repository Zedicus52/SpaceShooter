using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Abstraction
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonAction : MonoBehaviour
    { 
        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(Action);
        private void OnDisable() => _button.onClick.RemoveListener(Action);
        
        protected abstract void Action();
    }
}