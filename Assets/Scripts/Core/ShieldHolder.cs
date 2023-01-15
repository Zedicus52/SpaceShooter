using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Core
{
    [RequireComponent(typeof(PlayerHealth))]
    public class ShieldHolder : MonoBehaviour
    {
        [SerializeField] private int maxShieldsCount;
        [SerializeField] private List<Shield> shields;

        private int _shieldCount;

        private void OnEnable()
        {
            foreach (var t in shields)
            {
                t.ShieldDestroy += OnShieldDestroy;
            }
        }

        private void OnDisable()
        {
            foreach (var t in shields)
            {
                t.ShieldDestroy -= OnShieldDestroy;
            }
        }

        public void AddShield()
        {
            if(_shieldCount >= maxShieldsCount)
                return;
            shields[_shieldCount].gameObject.SetActive(true);
            _shieldCount += 1;
        }
        
        private void OnShieldDestroy()
        {
            _shieldCount -= 1;
        }
    }
}