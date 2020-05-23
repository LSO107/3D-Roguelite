using Player;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Character.Movement
{
    [RequireComponent(typeof(CharacterMovement))]
    internal sealed class CharacterUserInput : MonoBehaviour
    {
        private CharacterMovement m_CharacterMovement;
        private Transform m_Camera;

        public bool IsRunning { get; private set; }
        public bool IsFrozen;

        private void Start()
        {
            if (Camera.main != null)
                m_Camera = Camera.main.transform;

            m_CharacterMovement = GetComponent<CharacterMovement>();
        }

        public void ToggleRun()
        {
            IsRunning = !IsRunning;
        }

        private void FixedUpdate()
        {
            if (IsFrozen)
            {
                m_CharacterMovement.Move(new Vector3(0, 0, 0));
                return;
            }

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var camForward = Vector3.Scale(m_Camera.forward, new Vector3(1, 0, 1));
            var camRight = Vector3.Scale(m_Camera.right, new Vector3(1, 0, 1));
            var movement = vertical * camForward + horizontal * camRight;

            if (!IsRunning && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                movement *= 0.6f;
            }

            m_CharacterMovement.Move(movement);
        }
    }
}
