using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Character.Movement
{
    [RequireComponent(typeof(CharacterMovement))]
    internal sealed class CharacterUserInput : MonoBehaviour
    {
        private CharacterMovement m_CharacterMovement;
        private Transform m_Camera;

        [SerializeField] private Toggle m_Run;

        private void Start()
        {
            if (Camera.main != null)
                m_Camera = Camera.main.transform;

            m_CharacterMovement = GetComponent<CharacterMovement>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var camForward = Vector3.Scale(m_Camera.forward, new Vector3(1, 0, 1)).normalized;
            var camRight = Vector3.Scale(m_Camera.right, new Vector3(1, 0, 1)).normalized;
            var movement = vertical * camForward + horizontal * camRight;

            /*if (!m_Run.isOn)
            {
                movement *= 0.6f;
            }*/

            m_CharacterMovement.Move(movement);
        }
    }
}
