using System.Collections.Generic;
using System.Linq;
using Character.Movement;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class EnemyMovementLogic : MonoBehaviour
    {
        private CharacterMovement m_Movement;

        private Random m_Random;

        private IMovementBehaviour m_CurrentBehaviour;
        private IMovementBehaviour[] m_AllBehaviours;

        private List<IMovementBehaviour> m_ExecutingBehaviours;

        private EnemyCombatLogic m_EnemyCombat;

        private void Awake()
        {
            m_Movement = GetComponent<CharacterMovement>();
            m_Random = new Random();
            m_EnemyCombat = GetComponent<EnemyCombatLogic>();

            m_ExecutingBehaviours = new List<IMovementBehaviour>();
            m_AllBehaviours = GetComponents<IMovementBehaviour>();

            foreach (var behaviour in m_AllBehaviours)
            {
                behaviour.Initialize(m_Random, m_Movement);
            }
        }

        private void Update()
        {
            var data = AiDataGatherer.GetData(transform);

            foreach (var behaviour in m_AllBehaviours)
            {
                behaviour.ProcessData(data);
            }

            if (m_ExecutingBehaviours.Any())
            {
                if (m_CurrentBehaviour is CirclingBehaviour)
                {
                    var behaviour = m_ExecutingBehaviours.FirstOrDefault
                        (b => b is CirclingBehaviour == false);

                    if (behaviour != null)
                    {
                        ChangeBehaviour(behaviour);
                    }
                }

                if (m_CurrentBehaviour == null)
                {
                    ChangeBehaviour(m_ExecutingBehaviours.First());
                }
            }

            if (m_CurrentBehaviour == null)
            {
                m_Movement.Move(Vector3.zero);
            }
        }

        public void RegisterExecutionRequest(IMovementBehaviour behaviour)
        {
            if (!m_ExecutingBehaviours.Contains(behaviour))
            {
                m_ExecutingBehaviours.Add(behaviour);
            }
        }

        public void UnregisterExecutionRequest(IMovementBehaviour behaviour)
        {
            if (m_ExecutingBehaviours.Contains(behaviour))
            {
                m_ExecutingBehaviours.Remove(behaviour);
            }
        }

        public void BlockCombat()
        {
            m_EnemyCombat.BlockCombat();
        }

        public void UnblockCombat()
        {
            m_EnemyCombat.UnblockCombat();
        }

        public void StopCurrentRoutine()
        {
            m_CurrentBehaviour.Stop();
            m_CurrentBehaviour = null;
        }

        private void ChangeBehaviour(IMovementBehaviour behaviour)
        {
            m_CurrentBehaviour?.Stop();
            m_CurrentBehaviour = behaviour;
            m_CurrentBehaviour.Execute();
        }
    }
}
