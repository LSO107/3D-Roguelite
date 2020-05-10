using UnityEngine;

namespace Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        [SerializeField] private float m_MovementSpeed = 5f;
        private float m_RotationSpeed = 5f;

        [SerializeField] private float m_OffSetZ = -5;

        public GameObject target;
        public float rotateSpeed = 5;
        Vector3 offset;

        void Start()
        {
            offset = target.transform.position - transform.position;
        }

        void LateUpdate()
        {
            var horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            target.transform.Rotate(0, horizontal, 0);

            var desiredAngle = target.transform.eulerAngles.y;
            var rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position - (rotation * offset);

            transform.LookAt(target.transform);
        }
    }
}
