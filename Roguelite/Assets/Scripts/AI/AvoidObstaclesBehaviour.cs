using System.Collections.Generic;
using System.Linq;
using Character.Movement;
using Extensions;
using UnityEngine;
using Random = System.Random;

namespace AI
{
    internal sealed class AvoidObstaclesBehaviour : MonoBehaviour, IMovementBehaviour
    {
        private Random m_Random;
        private CharacterMovement m_CharacterMovement;
        private bool m_IsAvoidingObstacles;
        private AiDataObject m_LastFrameData;

        public void Initialize(Random random, CharacterMovement movement)
        {
            m_Random = random;
            m_CharacterMovement = movement;
        }

        private void Update()
        {
            if (!m_IsAvoidingObstacles)
                return;

            AvoidObstacles(m_LastFrameData.SurroundingTransforms);
        }

        public void ProcessData(AiDataObject dataObject)
        {
            m_LastFrameData = dataObject;
            var transforms = dataObject.SurroundingTransforms;

            if (transforms.Count > 0)
            {
                // Tell parent we want to go
            }
            else
            {
                // Tell parent we're ok to stop
            }
        }

        public void Execute()
        {
            m_IsAvoidingObstacles = true;
        }

        public void Stop()
        {
            m_IsAvoidingObstacles = false;
        }

        private void AvoidObstacles(IEnumerable<Transform> transforms)
        {
            m_IsAvoidingObstacles = true;
            var average = transforms.Select(t => t.position).GetMeanVector();

            var dir = (transform.position - average);
            m_CharacterMovement.Move(dir * 0.6f);
        }
    }
}
