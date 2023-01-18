using SpaceShooter.DataStructures;
using UnityEngine;

namespace SpaceShooter.Abstraction
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Bonus : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] protected AudioClip getSound;
        protected BorderFloat _border;
        private Transform _transform;

        private void Awake()
        {
            _border = new BorderFloat(ScreenBounds.LeftSide, ScreenBounds.RightSide, 
                ScreenBounds.DownSide, ScreenBounds.UpSide);
            _transform = transform;
            var rendered = GetComponent<SpriteRenderer>();
            _border.ExpendDownSide(rendered.sprite.bounds.size.y);
        }

        protected abstract void OnTriggerEnter2D(Collider2D col);

        private void Update()
        {
            if(_transform.position.y <= _border.DownSide)
                Destroy(gameObject);
            _transform.position = new Vector3(_transform.position.x,
                _transform.position.y - movementSpeed * Time.deltaTime, _transform.position.z);
        }
    }
}