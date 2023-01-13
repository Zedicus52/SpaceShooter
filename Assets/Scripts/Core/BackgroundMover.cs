using System;
using System.Collections.Generic;
using SpaceShooter.Abstraction;
using SpaceShooter.UI;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class BackgroundMover : MonoBehaviour, IPauseHandler
    {
        public bool IsPaused { get; private set; }
        [SerializeField] private float movementSpeed;
        [SerializeField] private List<BackgroundScaler> backgrounds;

        private Transform _transform;
        private Vector3 _startPosition;
        private Vector3 _endPosition;

      
        
        

        private void Start()
        {
            _transform = transform;
            _startPosition = _transform.position;
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            int y = Mathf.CeilToInt(screenSize.y)*2;
            _endPosition.y += -y;
            for (int i = 0; i < backgrounds.Count; i++)
            {
                backgrounds[i].gameObject.transform.position = new Vector3(0,i*y,0);
            }
            ProjectContext.Instance.PauseManager.Register(this);
        }

        private void Update()
        {
            if(IsPaused)
                return;
            float newY = _transform.position.y - movementSpeed * Time.deltaTime;
            
            if (newY <= _endPosition.y)
            {
                _transform.position = _startPosition;
                newY = _transform.position.y - movementSpeed * Time.deltaTime;
            }
            _transform.position = new Vector3(0, newY, 0);
        }

        public void SetPaused(bool isPaused) => IsPaused = isPaused;
        private void OnDestroy() => ProjectContext.Instance.PauseManager.UnRegister(this);
       
    }
}
