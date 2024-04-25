using Assets.Scripts.Helpers;
using Assets.Scripts.Inputs;
using Assets.Scripts.Ships;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

namespace Assets.Scripts.Managers
{
    public class CameraManager : Manager
    {
        private GameInputs m_inputActions;

        private float m_scrollDelta;
        private float m_zoom;
        private float m_minZoom = 10;
        private float m_maxZoom = 42;
        private float m_realMaxZoom = 32;
        private float m_smoothZoom = 0.1f;

        private Vector3 m_origin;
        private Vector3 m_diff;

        private Camera m_mainCamera;
        private PixelPerfectCamera m_pixelPerfectCamera;
        private bool m_isDragging = false;

        [SerializeField] private AnimationCurve m_easeTimeCruve;
        private bool m_easeToTarget = false;
        private Vector3 m_target;

        private bool m_followSelected;
        private Transform m_selectedObjToFollow;

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
            m_inputActions.BattleMap.ScrollWheelClick.performed += (_) => ToggleFollowSelectedShip();

            Events.Actions.OnSelected += OnSelected;
        }

        private void Update()
        {
            m_scrollDelta = m_inputActions.BattleMap.ScrollWheel.ReadValue<Vector2>().y;
            if (m_scrollDelta != 0) {
                m_zoom = Mathf.Clamp(m_scrollDelta, m_minZoom, m_maxZoom);
                m_pixelPerfectCamera.assetsPPU = (int)Mathf.Lerp(m_pixelPerfectCamera.assetsPPU, m_zoom, m_smoothZoom);
            }
        }

        private void FixedUpdate()
        {
            if (m_followSelected)
            {
                transform.position = Vector3.MoveTowards(m_mainCamera.transform.position, TargetCamPosTo(m_selectedObjToFollow.position), EvaluateCameraMovement());
                return;
            }
            
            if (m_isDragging) {
                m_diff = m_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
                transform.position = m_origin - m_diff;
            }

            if (m_easeToTarget) {
                transform.position = Vector3.MoveTowards(m_mainCamera.transform.position, m_target, EvaluateCameraMovement());
                if (Vector3.Distance(transform.position, m_target) <= 0.5f) m_easeToTarget = false;
            }
        }

        private void OnDestroy()
        {
            m_inputActions.BattleMap.RightClickDrag.started -= OnDrag;
            m_inputActions.BattleMap.RightClickDrag.canceled -= OnDrag;

            Events.Actions.OnSelected -= OnSelected;
        }

        private void OnDrag(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                m_origin = m_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            m_isDragging = ctx.performed || ctx.started;
        }

        private void OnSelected(Selectable obj)
        {
            m_easeToTarget = true;
            m_target = obj.transform.position;
            m_target = TargetCamPosTo(m_target);

            if (m_followSelected)
                m_selectedObjToFollow = obj.transform;
        }

        private void ToggleFollowSelectedShip()
        {
            m_followSelected = !m_followSelected;

            if (!m_followSelected) return;
            
            m_selectedObjToFollow = Main.Instance.GetManager<PlayerFleet>().SelectedShip.transform;
        }

        private Vector3 TargetCamPosTo(Vector3 objPos)
        {
            return new Vector3(objPos.x, objPos.y, m_mainCamera.transform.position.z);
        }

        private float EvaluateCameraMovement()
        {
            return m_easeTimeCruve.Evaluate(1f - (m_pixelPerfectCamera.assetsPPU / m_realMaxZoom));
        }
    }
}
