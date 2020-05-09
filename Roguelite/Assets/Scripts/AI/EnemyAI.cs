using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Combat;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class EnemyAI : MonoBehaviour
    {
        private IBehaviour[] m_Behaviours;
        private BehaviourState m_CurrentState;

        private Dictionary<IBehaviour, float> m_ExecutingBehaviours;
        private IBehaviour m_CurrentBehaviour;

        private CharacterCombat m_CharacterCombat;

        private Random m_Random;

        private bool m_CanPickNewBehaviour = true;

        private void Awake()
        {
            m_Behaviours = GetComponents<IBehaviour>();
            m_ExecutingBehaviours = new Dictionary<IBehaviour, float>();
            m_CharacterCombat = GetComponent<CharacterCombat>();
            m_Random = new Random();

            foreach (var behaviour in m_Behaviours)
            {
                behaviour.Initialize(m_Random);
            }
        }

        private void Update()
        {
            var data = AIDataGatherer.GetData(transform);

            foreach (var behaviour in m_Behaviours)
            {
                behaviour.ProcessData(data);
            }

            if (!m_CanPickNewBehaviour)
            {
                return;
            }

            if (m_ExecutingBehaviours.Any())
            {
                // Pick random behaviour and execute
                var next = m_Random.Next(0, m_ExecutingBehaviours.Count);
                var behaviour = m_ExecutingBehaviours.Keys.ElementAt(next);
                var time = m_ExecutingBehaviours[behaviour];

                ChangeBehaviour(behaviour, time);
            }
        }

        public void RegisterExecutionRequest(IBehaviour behaviour, float executionTime)
        {
            if (!m_ExecutingBehaviours.ContainsKey(behaviour))
            {
                m_ExecutingBehaviours.Add(behaviour, executionTime);
            }
            else
            {
                m_ExecutingBehaviours[behaviour] = executionTime;
            }
        }

        public void UnregisterExecutionRequest(IBehaviour behaviour)
        {
            if (m_ExecutingBehaviours.ContainsKey(behaviour))
            {
                m_ExecutingBehaviours.Remove(behaviour);
            }
        }

        public void UpdateCombatState(CombatState state)
        {
            m_CharacterCombat.CombatState = state;
        }

        private void ChangeBehaviour(IBehaviour newBehaviour, float time)
        {
            m_CurrentBehaviour?.Stop();

            newBehaviour.Execute();
            
            m_CurrentBehaviour = newBehaviour;
            m_ExecutingBehaviours = new Dictionary<IBehaviour, float>();
            StartCoroutine(BlockBehaviourChange(time));
        }

        private IEnumerator BlockBehaviourChange(float time)
        {
            print($"Blocking behaviour for {time} seconds.");

            m_CanPickNewBehaviour = false;
            yield return new WaitForSeconds(time);

            m_CanPickNewBehaviour = true;
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
