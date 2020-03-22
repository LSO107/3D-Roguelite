using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class EnemyAI : MonoBehaviour
    {
        private IBehaviour[] m_Behaviours;
        private BehaviourState m_CurrentState;

        private List<IBehaviour> m_ExecutingBehaviours;

        private Random m_Random;

        private void Awake()
        {
            m_Behaviours = GetComponents<IBehaviour>();
            m_ExecutingBehaviours = new List<IBehaviour>();

            foreach (var behaviour in m_Behaviours)
            {
                behaviour.Initialize();
            }

            m_Random = new Random();
        }

        private void Update()
        {
            var data = AIDataGatherer.GetData(transform);

            foreach (var behaviour in m_Behaviours)
            {
                behaviour.ProcessData(data);
            }

            if (m_ExecutingBehaviours.Any())
            {
                // Pick random behaviour and execute
                var next = m_Random.Next(0, m_ExecutingBehaviours.Count);

                Debug.Log($"{m_ExecutingBehaviours.Count} behaviours to choose from, rolled {next}");

                var behaviour = m_ExecutingBehaviours[next];
                behaviour.Execute();
                m_ExecutingBehaviours = new List<IBehaviour>();
            }
        }

        public void RegisterExecutionRequest(IBehaviour behaviour)
        {
            if (!m_ExecutingBehaviours.Contains(behaviour))
            {
                m_ExecutingBehaviours.Add(behaviour);
            }
        }

        private bool IsInState(params BehaviourState[] states)
        {
            return states.Any(state => m_CurrentState == state);
        }

        private void TransitionState(BehaviourState newState)
        {
            if (m_CurrentState == newState)
            {
                return;
            }

            m_CurrentState = newState;

            UpdateChildrenStates();
            Debug.Log(newState);
        }

        private void UpdateChildrenStates()
        {
            foreach (var behaviour in m_Behaviours)
            {
                behaviour.UpdateState(m_CurrentState);
            }
        }
    }
}
