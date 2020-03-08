using UnityEngine;
using UnityEngine.UI;

namespace Player.Movement
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    internal sealed class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character;
        private Transform m_Camera;

        [SerializeField] private Toggle m_Run;

        private void Start()
        {
            if (Camera.main != null)
                m_Camera = Camera.main.transform;

            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var camForward = Vector3.Scale(m_Camera.forward, new Vector3(1, 0, 1)).normalized;
            var movement = vertical * camForward + horizontal * m_Camera.right;

            if (!m_Run.isOn)
            {
                movement *= 0.5f;
            }

            m_Character.Move(movement);
        }
    }
}
