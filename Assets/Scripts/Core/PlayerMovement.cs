using SpaceShooter.Abstraction;
using SpaceShooter.UI;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class PlayerMovement : MonoBehaviour, IPauseHandler
    {
        public bool IsPaused { get; private set; }
        [SerializeField] private float horizontalMovementSpeed;
        [SerializeField] private float verticalMovementSpeed;
        [SerializeField] private Joystick joystick;
        private Vector2 _bounds;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            CalculateBounds();
            ProjectContext.Instance.PauseManager.Register(this);
        }

        private void Update()
        {
            if(IsPaused)
                return;
            Vector3 delta = new Vector3
            {
                x = joystick.Horizontal * horizontalMovementSpeed * Time.deltaTime,
                y = joystick.Vertical * verticalMovementSpeed * Time.deltaTime
            };
            Vector3 newPosition = _transform.position + delta;
            newPosition.x = Mathf.Clamp(newPosition.x, -_bounds.x, _bounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, -_bounds.y, _bounds.y);
            _transform.position = newPosition;

        }

        private void CalculateBounds()
        {
            Camera main = Camera.main;
            Vector3 vec = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _bounds.x = vec.x;
            _bounds.y = vec.y;
        }

        public void SetPaused(bool isPaused) => IsPaused = isPaused;
        private void OnDestroy() => ProjectContext.Instance.PauseManager.UnRegister(this);

    }
}
