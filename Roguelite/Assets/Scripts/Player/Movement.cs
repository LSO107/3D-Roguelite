using UnityEngine;

namespace Player
{
    internal sealed class Movement : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;

        [SerializeField] private float m_MovementSpeed = 1;

        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            m_Rigidbody.MovePosition(GetTargetPosition());
        }

        private Vector3 GetTargetPosition()
        {
            return transform.position += GetTargetDirection() * (m_MovementSpeed * Time.deltaTime);
        }

        private static Vector3 GetTargetDirection()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            return new Vector3(horizontal, 0, vertical).normalized;
        }
    }
}
