using UnityEngine;

namespace Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        [SerializeField] private float m_MovementSpeed = 5f;

        [SerializeField] private float m_OffSetZ = -5;

        private void Update()
        {
            var pos = transform.position;
            var playerPos = m_Player.position;
            var targetPos = new Vector3(playerPos.x, pos.y, playerPos.z + m_OffSetZ);

            transform.position = Vector3.MoveTowards(pos, targetPos, m_MovementSpeed * Time.deltaTime);
        }
    }
}
