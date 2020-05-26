using Character.Movement;
using UnityEngine;
using Random = System.Random;

namespace AI
{
    internal sealed class ChargeBehaviour : MonoBehaviour, IMovementBehaviour
    {
        private Random m_Random;
        private CharacterMovement m_CharacterMovement;
        private EnemyMovementLogic m_Parent;
        private AiDataObject m_LastFrameData;
        private bool m_IsCharging;
        private Vector3 m_ChargeLocation;

        private float m_ChargeTime = 4f;
        private float m_CurrentTime;

        public void Initialize(Random random, CharacterMovement movement)
        {
            m_Parent = GetComponent<EnemyMovementLogic>();
            m_Random = random;
            m_CharacterMovement = movement;
        }

        private void Update()
        {
            if (!m_IsCharging)
                return;

            m_CurrentTime += Time.deltaTime;

            if (   m_LastFrameData.DistanceFromPlayer <= 2f
                || Vector3.Distance(transform.position, m_ChargeLocation) <= 2f 
                || m_CurrentTime >= m_ChargeTime)
            {
                m_Parent.StopCurrentRoutine();
                m_IsCharging = false;
            }
            else
            {
                Charge();
            }
        }

        public void ProcessData(AiDataObject dataObject)
        {
            m_LastFrameData = dataObject;

            var percentage = m_LastFrameData.Health.MaxHealth / 100 * 15;

            if (dataObject.DistanceFromPlayer >= 8f && m_LastFrameData.Health.DamageTaken <= percentage)
            {
                m_Parent.RegisterExecutionRequest(this);
            }
            else
            {
                m_Parent.UnregisterExecutionRequest(this);
            }
        }

        public void Execute()
        {
            m_IsCharging = true;
            m_ChargeLocation = m_LastFrameData.PlayerLocation;
            m_Parent.BlockCombat();
        }

        public void Stop()
        {
            m_IsCharging = false;
            m_CurrentTime = 0;
            m_Parent.UnblockCombat();
        }

        private void Charge()
        {
            m_CharacterMovement.Move(m_ChargeLocation - transform.position);
            transform.LookAt(m_ChargeLocation);
        }
    }
}
