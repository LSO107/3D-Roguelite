using System.Collections;
using Character.Combat;
using Extensions;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class AttackingBehaviour : MonoBehaviour, IBehaviour
    {
        private EnemyAI m_EnemyAI;
        private BehaviourState m_CurrentState;
        private Random m_Random;
        private Animator m_Animator;

        private float m_AttackTime = 2;
        private float m_CurrentTime;

        private const float MinimumAttackTime = 2.0f;
        private const float MaximumAttackTime = 6.0f;

        private bool m_CanAttack;

        public void Initialize(Random random)
        {
            m_EnemyAI = GetComponent<EnemyAI>();
            m_Animator = GetComponent<Animator>();
            m_Random = random;
        }

        public void Update()
        {
            if (!m_CanAttack)
                return;

            StartCoroutine(Attack());
        }

        public void UpdateState(BehaviourState state)
        {
            m_CurrentState = state;
        }

        public void Execute()
        {
            m_CanAttack = true;
            Debug.Log("Attacking now.");
            m_EnemyAI.UpdateCombatState(CombatState.Attacking);
        }

        public void Stop()
        {
            m_CanAttack = false;
        }

        public void ProcessData(AIDataObject data)
        {
            if (data.DistanceFromPlayer <= 5)
            {
                m_CurrentTime += Time.deltaTime;

                if (m_CurrentTime >= m_AttackTime)
                {
                    var randomTime = m_Random.RandomFloat(MinimumAttackTime, MaximumAttackTime);
                    m_EnemyAI.RegisterExecutionRequest(this, randomTime);
                    m_CurrentTime = 0f;
                }
            }
            else
            {
                m_EnemyAI.UnregisterExecutionRequest(this);
                m_CurrentTime = 0;
            }
        }

        private IEnumerator Attack()
        {
            m_Animator.SetInteger("Attack Type", 0);
            m_Animator.SetTrigger("Attack");
            yield return new WaitUntil(() => m_Animator.GetBool("Attack") == false);
        }
    }
}
