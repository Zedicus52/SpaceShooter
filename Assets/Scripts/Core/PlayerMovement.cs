using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class PlayerMovement : MonoBehaviour, IPauseHandler
    {
        public bool IsPaused { get; private set; }
        [SerializeField] private float horizontalMovementSpeed;
        private Vector2 _bounds;
        private Transform _transform;
        private Vector3 _oldPos;

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
            
            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                if(touch.phase != TouchPhase.Moved)
                    return;
                var touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;
                touchPos.x = Mathf.Clamp(touchPos.x, -_bounds.x, _bounds.x);
                touchPos.y = Mathf.Clamp(touchPos.y, -_bounds.y, _bounds.y);
                if (Mathf.Abs(_oldPos.x - touchPos.x) < 1.0f || Mathf.Abs(_oldPos.y - touchPos.y) < 1.0f)
                {
                    _transform.position = Vector3.Lerp(_transform.position, touchPos,
                        horizontalMovementSpeed * Time.deltaTime);
                    _oldPos = touchPos;
                }
            }
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
