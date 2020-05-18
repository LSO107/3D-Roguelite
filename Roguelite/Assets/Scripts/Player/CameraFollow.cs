using UnityEngine;

namespace Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        private float m_RotationSpeed = 5f;

        [SerializeField] private float m_OffSetZ = -3;

        public float rotateSpeed = 5;
        Vector3 offset;

        private Vector3 m_TargetPosition;

        private CameraMode m_CameraMode;

        private void Start()
        {
            offset = m_Player.position - transform.position;
            FreeCamera();
        }

        private void LateUpdate()
        {
            if (m_CameraMode == CameraMode.Locked)
                return;

            var horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            m_Player.transform.Rotate(0, horizontal, 0);

            var desiredAngle = m_Player.eulerAngles.y;
            var rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = m_Player.position - (rotation * offset);

            transform.LookAt(m_Player.position + new Vector3(0, 1.5f, 0));
        }

        public void LockCamera()
        {
            m_CameraMode = CameraMode.Locked;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void FreeCamera()
        {
            m_CameraMode = CameraMode.Free;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    internal enum CameraMode
    {
        Free,
        Locked
    }
}
