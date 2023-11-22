using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

namespace Assets.Scripts.Managers
{
    public class CameraManager : Manager
    {
        private GameInputs m_inputActions;

        private float scrollDelta;
        private float zoom;
        private float minZoom = 6;
        private float maxZoom = 42;
        private float smoothZoom = 0.1f;

        private Vector3 m_origin;
        private Vector3 m_diff;

        private Camera m_mainCamera;
        private PixelPerfectCamera m_pixelPerfectCamera;
        private bool m_isDragging = false;

        private void Awake()
        {
            m_mainCamera = Camera.main;
            m_pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
            m_inputActions = new GameInputs();
        }

        private void OnEnable()
        {
            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Disable();
        }

        private void Start()
        {
            m_inputActions.BattleMap.RightClickDrag.started += OnDrag;
            m_inputActions.BattleMap.RightClickDrag.canceled += OnDrag;
        }

        private void Update()
        {
            scrollDelta = m_inputActions.BattleMap.ScrollWheel.ReadValue<Vector2>().y;
            if (scrollDelta != 0) {
                zoom = Mathf.Clamp(scrollDelta, minZoom, maxZoom);
                m_pixelPerfectCamera.assetsPPU = (int)Mathf.Lerp(m_pixelPerfectCamera.assetsPPU, zoom, smoothZoom);
            }
        }

        private void FixedUpdate()
        {
            if (!m_isDragging) return;

            m_diff = m_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            transform.position = m_origin - m_diff;
        }

        private void OnDestroy()
        {
            m_inputActions.BattleMap.RightClickDrag.started -= OnDrag;
            m_inputActions.BattleMap.RightClickDrag.canceled -= OnDrag;
        }

        private void OnDrag(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                m_origin = m_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            m_isDragging = ctx.performed || ctx.started;
        }
    }
}
