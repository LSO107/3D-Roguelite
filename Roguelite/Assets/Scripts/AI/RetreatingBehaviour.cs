using Character.Health;
using Character.Movement;
using UnityEngine;

using Random = System.Random;

namespace AI
{
    internal sealed class RetreatingBehaviour : MonoBehaviour, IMovementBehaviour
    {
        private Random m_Random;
        private bool m_IsRetreating;
        private CharacterMovement m_Movement;
        private AiDataObject m_LastFrameData;
        private EnemyMovementLogic m_Parent;

        private float m_RetreatTime = 4;
        private float m_CurrentTime;

        public void Initialize(Random random, CharacterMovement movement)
        {
            m_Parent = GetComponent<EnemyMovementLogic>();
            m_Random = random;
            m_Movement = movement;
        }

        private void Update()
        {
            if (!m_IsRetreating)
                return;

            m_CurrentTime += Time.deltaTime;

            Retreat(m_LastFrameData.PlayerLocation);

            if (   m_LastFrameData.DistanceFromPlayer >= 8f
                || m_CurrentTime >= m_RetreatTime)
            {
                m_Parent.UnregisterExecutionRequest(this);
                m_Parent.StopCurrentRoutine();
                m_IsRetreating = false;
            }
        }

        public void ProcessData(AiDataObject dataObject)
        {
            m_LastFrameData = dataObject;

            var healthPercentage = (dataObject.Health.MaxHealth / 100) * 30;

            if (dataObject.Health.DamageTaken >= healthPercentage)
            {
                m_Parent.RegisterExecutionRequest(this);
            }
        }

        public void Execute()
        {
            m_IsRetreating = true;
            m_Parent.BlockCombat();
        }

        public void Stop()
        {
            m_IsRetreating = false;
            m_CurrentTime = 0;
            m_Parent.UnblockCombat();
        }

        private void Retreat(Vector3 playerLocation)
        {
            var myPos = transform.position;
            var dir = myPos - playerLocation;
            m_Movement.Move(dir * 0.6f);
            transform.LookAt(myPos + dir);
        }
    }
}
