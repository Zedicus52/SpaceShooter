using SpaceShooter.Abstraction;
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

        private void OnNewWeaponSet(PlayerWeapon weapon)
        {
            backgroundRenderer.sprite = weapon.WeaponIconBackground;
            iconRenderer.sprite = weapon.WeaponIcon;
        }
    }
}