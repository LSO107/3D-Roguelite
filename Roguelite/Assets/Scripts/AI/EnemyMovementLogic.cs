using System.Collections.Generic;
using System.Linq;
using Character.Movement;
using Extensions;
using UnityEngine;
using Random = System.Random;

namespace AI
{
    internal sealed class EnemyMovementLogic : MonoBehaviour
    {
        private CharacterMovement m_Movement;

        private float m_ChangeHorizontalTime = 3f;
        private float m_ChangeVerticalTime = 3f;

        private float m_VerticalTime;
        private float m_HorizontalTime;

        private bool m_IsAvoidingObstacles;

        private Vector3 m_HorizontalDirection;
        private Vector3 m_VerticalDirection;

        private Random m_Random;

        private void Awake()
        {
            m_Movement = GetComponent<CharacterMovement>();
            m_Random = new Random();
        }

        private void Update()
        {
            // Gather AI data
            // Make decisions based on moving left/right/towards and away from player
            var data = AiDataGatherer.GetData(transform);

            transform.LookAt(data.PlayerLocation);

            var transforms = data.SurroundingTransforms;

            if (transforms.Count > 0)
            {
                AvoidObstacles(transforms);
            }
            else
            {
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

                if (m_IsAvoidingObstacles)
                {
                    m_HorizontalDirection = GetRandomDirection(horizontalDirections);
                    m_VerticalDirection = GetRandomDirection(verticalDirections);
                    m_IsAvoidingObstacles = false;
                }
                else
                {
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
                }
            }

            m_Movement.Move((m_HorizontalDirection + m_VerticalDirection) * 0.6f);
        }

        private void Retreat()
        {
            // Is my health low?

            // Have I lost X health in the past X seconds?

            // Move back from player + block?
        }

        private void Charge()
        {
            // Enough distance from player?

            // Move to player at X speed

            // Chance to attack or block at end of charge?
        }

        private void Circle()
        {

        }

        private void AvoidObstacles(IEnumerable<Transform> transforms)
        {
            m_IsAvoidingObstacles = true;
            var average = transforms.Select(t => t.position).GetMeanVector();

            var dir = (transform.position - average);
            m_Movement.Move(dir * 0.6f);
        }

        private Vector3 GetRandomDirection(IEnumerable<Vector3> directions)
        {
            return directions.GetRandomVector(m_Random);
        }
    }
}
