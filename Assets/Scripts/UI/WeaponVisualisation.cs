using SpaceShooter.Core;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public class WeaponVisualisation : MonoBehaviour
    {
        [SerializeField] private PlayerShooter shooter;
        [SerializeField] private Image backgroundRenderer;
        [SerializeField] private Image iconRenderer;

        private void OnEnable() => shooter.NewWeaponSet += OnNewWeaponSet;
        private void OnDisable() => shooter.NewWeaponSet -= OnNewWeaponSet;

        private void OnNewWeaponSet(Sprite icon, Sprite background)
        {
            backgroundRenderer.sprite = background;
            iconRenderer.sprite = icon;
        }
    }
}