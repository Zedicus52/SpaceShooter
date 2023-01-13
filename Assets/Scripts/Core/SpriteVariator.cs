using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteVariator : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;

        private SpriteRenderer _renderer;

        private void Awake() => _renderer = GetComponent<SpriteRenderer>();

        private void OnEnable()
        {
            _renderer.sprite = sprites[Random.Range(0, sprites.Count)];
        }
    }
}