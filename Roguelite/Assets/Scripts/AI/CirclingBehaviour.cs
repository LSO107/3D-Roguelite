using UnityEngine;

namespace AI
{
    internal sealed class CirclingBehaviour : MonoBehaviour, IBehaviour
    {
        private EnemyAI m_EnemyAI;
        [SerializeField] private Transform m_Target;
        private BehaviourState m_CurrentState;
        private float m_Rad = 2;
        private float m_Angle = 180;

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
            Debug.Log("Circling");
        }

        public void ProcessData(AIDataObject data)
        {
            if (m_CurrentState != BehaviourState.Circling)
                return;

            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = GetNextPosition(m_Rad, m_Angle);
                m_Angle++;

                if (m_Angle > 360f)
                {
                    m_Angle -= 360f;
                }
            }   
        }

        private Vector3 GetNextPosition(float radius, float angle)
        {
            //summon the enemies around this central GameObject
            var a = angle * Mathf.PI * 2f / 360;
            var ePosition = new Vector3(radius * Mathf.Cos(a), transform.position.y, radius * Mathf.Sin(a));
            return m_Target.position + ePosition;
        }
    }
}
