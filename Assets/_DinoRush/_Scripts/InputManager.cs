using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
    /// A simple Input Manager for a Runner game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        private PlayerController _playerController;
        /// <summary>
        /// Returns the InputManager.
        /// </summary>
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        [SerializeField]
        float m_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;

        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }
        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
#if UNITY_EDITOR
            m_InputPosition = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.isPressed)
            {
                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
#else
            if (Touch.activeTouches.Count > 0)
            {
                m_InputPosition = Touch.activeTouches[0].screenPosition;

                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
#endif
            if (m_HasInput)
            {
                float normalizedDeltaPosition = (m_InputPosition.x - m_PreviousInputPosition.x) 
                    / Screen.width * m_InputSensitivity;
                _playerController.SetDeltaPosition(normalizedDeltaPosition);
            }
            m_PreviousInputPosition = m_InputPosition;
        }

        
    }

