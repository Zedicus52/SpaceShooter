using UnityEngine;

namespace SpaceShooter.Core
{
    public class BackgroundScaler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            int x = Mathf.CeilToInt( screenSize.x)*2;
            int y = Mathf.CeilToInt( screenSize.y)*2;
            _spriteRenderer.size = new Vector2(x, y);
        }
    }
}
