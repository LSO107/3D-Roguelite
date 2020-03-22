using UnityEngine;

namespace AI
{
    internal sealed class AttackingBehaviour : MonoBehaviour, IBehaviour
    {
        private EnemyAI m_EnemyAI;
        private BehaviourState m_CurrentState;

        private float m_AttackTime = 2;
        private float m_CurrentTime;

        public void Initialize()
        {
            m_EnemyAI = GetComponent<EnemyAI>();
        }

        public void UpdateState(BehaviourState state)
        {
            m_CurrentState = state;
        }

        public void Execute()
        {
            Debug.Log("Attacking now.");
            m_CurrentTime = 0f;
        }

        public void ProcessData(AIDataObject data)
        {
            if (data.DistanceFromPlayer <= 2)
            {
                m_CurrentTime += Time.deltaTime;

                if (m_CurrentTime >= m_AttackTime)
                {
                    m_EnemyAI.RegisterExecutionRequest(this);
                }
            }
            else
            {
                m_CurrentTime = 0;
            }
        }
    }
}
