using System;
using Character.Movement;
using UnityEngine;

namespace Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;

        [SerializeField] private float rotateSpeed = 5;
        private Vector3 m_Offset;

        private float heightOffset = 1.5f;

        private CameraMode m_CameraMode;

        private void Start()
        {
            m_Offset = m_Player.position - transform.position;
            FreeCamera();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (m_CameraMode == CameraMode.Free)
                {
                    LockCamera();
                }
                else
                {
                    FreeCamera();
                }
            }

            if (m_CameraMode == CameraMode.Locked)
            {
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");

                if (Math.Abs(horizontal) > 0 || Math.Abs(vertical) > 0)
                    FreeCamera();
            }
        }

        private void LateUpdate()
        {
            if (m_CameraMode == CameraMode.Locked)
                return;

            var horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            var vertical = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            heightOffset += vertical;
            heightOffset = Mathf.Clamp(heightOffset, 0.5f, 1.5f);
            m_Player.transform.Rotate(0, horizontal, 0);

            var desiredAngle = m_Player.eulerAngles.y;
            var rotation = Quaternion.Euler(0, desiredAngle, 0);
            // TODO: use height offset here
            transform.position = m_Player.position - (rotation * m_Offset);

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
