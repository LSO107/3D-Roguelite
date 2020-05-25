using System.Collections.Generic;
using Character.Movement;
using Extensions;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class CirclingBehaviour : MonoBehaviour, IMovementBehaviour
    {
        private Random m_Random;
        private CharacterMovement m_CharacterMovement;
        private EnemyMovementLogic m_Parent;
        private bool m_IsCircling;

        private AiDataObject m_LastFrameData;

        private float m_ChangeHorizontalTime = 3f;
        private float m_ChangeVerticalTime = 3f;

        private float m_VerticalTime;
        private float m_HorizontalTime;

        private Vector3 m_HorizontalDirection;
        private Vector3 m_VerticalDirection;

        public void Initialize(Random random, CharacterMovement movement)
        {
            m_Parent = GetComponent<EnemyMovementLogic>();
            m_Random = random;
            m_CharacterMovement = movement;
        }

        private void Update()
        {
            if (!m_IsCircling)
                return;

            transform.LookAt(m_LastFrameData.PlayerLocation);

            var right = transform.right;
            var forward = transform.forward;

            var horizontalDirections = new[]
            {
                right,
                -right,
                Vector3.zero
            };

            var verticalDirections = new[]
            {
                forward,
                -forward,
                Vector3.zero
            };

            m_HorizontalTime += Time.deltaTime;
            m_VerticalTime += Time.deltaTime;

            if (m_HorizontalTime >= m_ChangeHorizontalTime)
            {
                m_HorizontalDirection = GetRandomDirection(horizontalDirections);
                m_HorizontalTime = 0;
            }

            if (m_VerticalTime >= m_ChangeVerticalTime)
            {
                m_VerticalDirection = GetRandomDirection(verticalDirections);
                m_VerticalTime = 0;
            }

            m_CharacterMovement.Move((m_HorizontalDirection + m_VerticalDirection) * 0.6f);
        }
        
        public void ProcessData(AiDataObject dataObject)
        {
            m_LastFrameData = dataObject;
            m_Parent.RegisterExecutionRequest(this);
        }

        public void Execute()
        {
            m_IsCircling = true;
        }

        public void Stop()
        {
            m_IsCircling = false;
        }

        private Vector3 GetRandomDirection(IEnumerable<Vector3> directions)
        {
            return directions.GetRandomVector(m_Random);
        }
    }
}
