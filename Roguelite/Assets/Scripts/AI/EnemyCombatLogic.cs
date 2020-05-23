using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Combat;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class EnemyCombatLogic : MonoBehaviour
    {
        private ICombatBehaviour[] m_CombatBehaviours;
        private CombatState m_CurrentState;

        private Dictionary<ICombatBehaviour, float> m_ExecutingCombatBehaviours;
        private ICombatBehaviour m_CombatBehaviour;

        private CharacterCombat m_CharacterCombat;

        public Animator Animator;

        private Random m_Random;

        private bool m_CanPickNewBehaviour = true;

        private void Awake()
        {
            m_CombatBehaviours = GetComponents<ICombatBehaviour>();
            m_ExecutingCombatBehaviours = new Dictionary<ICombatBehaviour, float>();
            m_CharacterCombat = GetComponent<CharacterCombat>();
            Animator = GetComponent<Animator>();
            m_Random = new Random();

            foreach (var behaviour in m_CombatBehaviours)
            {
                behaviour.Initialize(m_Random);
            }
        }

        private void Update()
        {
            var data = AiDataGatherer.GetData(transform);

            foreach (var behaviour in m_CombatBehaviours)
            {
                behaviour.ProcessData(data);
            }

            if (!m_CanPickNewBehaviour)
            {
                return;
            }

            if (m_ExecutingCombatBehaviours.Any())
            {
                // Pick random behaviour and execute
                var next = m_Random.Next(0, m_ExecutingCombatBehaviours.Count);
                var behaviour = m_ExecutingCombatBehaviours.Keys.ElementAt(next);
                var time = m_ExecutingCombatBehaviours[behaviour];

                ChangeBehaviour(behaviour, time);
            }
        }

        public void RegisterExecutionRequest(ICombatBehaviour behaviour, float executionTime)
        {
            if (!m_ExecutingCombatBehaviours.ContainsKey(behaviour))
            {
                m_ExecutingCombatBehaviours.Add(behaviour, executionTime);
            }
            else
            {
                m_ExecutingCombatBehaviours[behaviour] = executionTime;
            }
        }

        public void UnregisterExecutionRequest(ICombatBehaviour behaviour)
        {
            if (m_ExecutingCombatBehaviours.ContainsKey(behaviour))
            {
                m_ExecutingCombatBehaviours.Remove(behaviour);
            }
        }

        public void UpdateCombatState(Character.Combat.CombatState state)
        {
            m_CharacterCombat.CombatState = state;
        }

        private void ChangeBehaviour(ICombatBehaviour newBehaviour, float time)
        {
            m_CombatBehaviour?.Stop();

            newBehaviour.Execute();
            
            m_CombatBehaviour = newBehaviour;
            m_ExecutingCombatBehaviours = new Dictionary<ICombatBehaviour, float>();
            StartCoroutine(BlockBehaviourChange(time));
        }

        private IEnumerator BlockBehaviourChange(float time)
        {
            m_CanPickNewBehaviour = false;
            yield return new WaitForSeconds(time);

            m_CanPickNewBehaviour = true;
        }
    }
}
