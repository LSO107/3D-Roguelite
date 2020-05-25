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

        private Dictionary<ICombatBehaviour, float> m_ExecutingCombatBehaviours;
        private ICombatBehaviour m_CurrentBehaviour;

        private CharacterCombat m_CharacterCombat;

        private EnemyMovementLogic m_Movement;

        public Animator Animator;

        private Random m_Random;

        private bool m_CanPickNewBehaviour = true;

        private const int MinIdleTime = 4;
        private const int MaxIdleTime = 10;

        private void Awake()
        {
            m_CombatBehaviours = GetComponents<ICombatBehaviour>();
            m_ExecutingCombatBehaviours = new Dictionary<ICombatBehaviour, float>();
            m_CharacterCombat = GetComponent<CharacterCombat>();
            m_Movement = GetComponent<EnemyMovementLogic>();
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
                var chance = m_Random.Next(0, 100);

                if (chance <= 25)
                {
                    var idleTime = m_Random.Next(MinIdleTime, MaxIdleTime);
                    Debug.Log($"Idling for {idleTime} seconds");
                    m_CurrentBehaviour?.Stop();
                    StartCoroutine(BlockBehaviourChange(idleTime));
                    m_CharacterCombat.UpdateState(CombatState.None);
                    return;
                }

                // Pick random behaviour and execute
                var next = m_Random.Next(0, m_ExecutingCombatBehaviours.Count);
                var behaviour = m_ExecutingCombatBehaviours.Keys.ElementAt(next);
                var time = m_ExecutingCombatBehaviours[behaviour];

                ChangeBehaviour(behaviour, time);
            }
        }

        public void BlockCombat()
        {
            m_CurrentBehaviour?.Stop();
            m_CanPickNewBehaviour = false;
        }

        public void UnblockCombat()
        {
            m_CanPickNewBehaviour = true;
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

        public void UpdateCombatState(CombatState state)
        {
            m_CharacterCombat.UpdateState(state);
        }

        private void ChangeBehaviour(ICombatBehaviour newBehaviour, float time)
        {
            m_CurrentBehaviour?.Stop();

            newBehaviour.Execute();
            
            m_CurrentBehaviour = newBehaviour;
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
