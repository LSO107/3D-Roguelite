using System.Collections;
using Character.Combat;
using Extensions;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class AttackingBehaviour : MonoBehaviour, ICombatBehaviour
    {
        private EnemyCombatLogic m_EnemyCombat;
        private CombatState m_CurrentState;
        private Random m_Random;
        private Animator m_Animator;

        private float m_AttackTime = 2;
        private float m_CurrentTime;

        private const float MinimumAttackTime = 2.0f;
        private const float MaximumAttackTime = 6.0f;

        private bool m_CanAttack;

        public void Initialize(Random random)
        {
            m_EnemyCombat = GetComponent<EnemyCombatLogic>();
            m_Animator = GetComponent<Animator>();
            m_Random = random;
        }

        public void Update()
        {
            if (!m_CanAttack)
                return;

            // If some bool is true
            StartCoroutine(Attack());
        }

        public void UpdateState(CombatState state)
        {
            m_CurrentState = state;
        }

        public void Execute()
        {
            m_CanAttack = true;
            Debug.Log("Attacking now.");
            m_EnemyCombat.UpdateCombatState(Character.Combat.CombatState.Attacking);
        }

        public void Stop()
        {
            m_CanAttack = false;
        }

        public void ProcessData(AiDataObject data)
        {
            if (data.DistanceFromPlayer <= 5)
            {
                m_CurrentTime += Time.deltaTime;

                if (m_CurrentTime >= m_AttackTime)
                {
                    var randomTime = m_Random.RandomFloat(MinimumAttackTime, MaximumAttackTime);
                    m_EnemyCombat.RegisterExecutionRequest(this, randomTime);
                    m_CurrentTime = 0f;
                }
            }
            else
            {
                m_EnemyCombat.UnregisterExecutionRequest(this);
                m_CurrentTime = 0;
            }
        }

        private IEnumerator Attack()
        {
            // Set some bool to true
            // Set the attack int
            // Wait until the attack int is back to 0
            yield return new WaitUntil(() => m_Animator.GetBool("") == false);

            // Set some bool to false
        }
    }
}
