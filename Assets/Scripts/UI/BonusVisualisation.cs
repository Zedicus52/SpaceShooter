using System;
using TMPro;
using UnityEngine;

namespace SpaceShooter.UI
{
    public abstract class BonusVisualisation : MonoBehaviour
    {
        [SerializeField] protected TMP_Text text;

        protected abstract void Awake();
        protected abstract void OnEnable();
        protected abstract void OnDisable();

    }
}