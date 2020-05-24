using System.Collections;
using Extensions;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class AttackingBehaviour : MonoBehaviour, ICombatBehaviour
    {
        private EnemyCombatLogic m_EnemyCombat;
        private Random m_Random;
        private Animator m_Animator;

        private float m_AttackTime = 2;
        private float m_CurrentTime;

        private const float MinimumAttackTime = 2.0f;
        private const float MaximumAttackTime = 6.0f;

        private bool m_CanAttack;
        private bool m_IsAttacking;

        private static readonly int AttackInt = Animator.StringToHash("Attack");

        public void Initialize(Random random)
        {
            m_EnemyCombat = GetComponent<EnemyCombatLogic>();
            m_Animator = GetComponent<Animator>();
            m_Random = random;
        }

        private void OnGUI()
        {
            GUILayout.Label($"Attack: {m_Animator.GetInteger(AttackInt)}");
            GUILayout.Label($"Can Attack: {m_CanAttack}");
        }

        public void Update()
        {
            if (!m_CanAttack || m_IsAttacking)
                return;

            Debug.Log("Doing attack :D");
            // If some bool is true
            StartCoroutine(Attack());
        }

        public void Execute()
        {
            m_CanAttack = true;
            m_EnemyCombat.UpdateCombatState(Character.Combat.CombatState.Attacking);
        }

        public void Stop()
        {
            Debug.Log("Stopping attacking??");
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
            m_IsAttacking = true;

            var rnd = m_Random.Next(1, 4);
            m_Animator.SetInteger(AttackInt, rnd);

            yield return new WaitUntil(() => m_Animator.GetInteger(AttackInt) == 0);

            m_IsAttacking = false;
        }
    }
}
