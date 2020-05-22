using Character.Movement;
using UnityEngine;

namespace AI
{
    internal sealed class EnemyAIMovement : MonoBehaviour
    {
        private CharacterMovement m_Movement;

        private void Awake()
        {
            m_Movement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            // Gather AI data
            // Make decisions based on moving left/right/towards and away from player
            var data = AIDataGatherer.GetData(transform);

            var ffff = data.SurroundingTransforms;

            foreach (var f in ffff)
            {

            }

            transform.LookAt(data.PlayerLocation);

            var movementVector = new Vector3(0, 0, 0);

            var rightMovement = Vector3.Scale(transform.right, new Vector3(1, 0, 1));

            rightMovement *= 0.6f;

            m_Movement.Move(rightMovement);
        }
    }
}
