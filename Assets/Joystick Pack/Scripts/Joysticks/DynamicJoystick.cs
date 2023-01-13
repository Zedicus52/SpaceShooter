﻿using SpaceShooter.Core;
using UnityEngine;
using UnityEngine.EventSystems;


    public class DynamicJoystick : Joystick
    {
        public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

        [SerializeField] private float moveThreshold = 1;

        protected override void Start()
        {
            //RectTransform rectTransform = GetComponent<RectTransform>();
            //rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Screen.height / 2.0f);
            MoveThreshold = moveThreshold;
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            //background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > moveThreshold)
            {
                Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }
            base.HandleInput(magnitude, normalised, radius, cam);
        }
    }
