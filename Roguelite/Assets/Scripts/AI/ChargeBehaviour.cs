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

            // probably add timer to stop it charging forever if player dodges
            if (m_LastFrameData.DistanceFromPlayer <= 2f || Vector3.Distance(transform.position, m_ChargeLocation) <= 2f)
            {
                Debug.Log("Should stop charge now :D");
                m_Parent.StopCurrentRoutine();
                m_IsCharging = false;
            }
            else
            {
                Charge();
            }
        }

        private void OnGUI()
        {
            GUILayout.Label($"Me: {transform.position}");
            GUILayout.Label($"Player: {m_LastFrameData.PlayerLocation}");
            GUILayout.Label($"Charge: {m_ChargeLocation}");
            GUILayout.Label($"Charging: {m_IsCharging}");
        }

        public void ProcessData(AiDataObject dataObject)
        {
            m_LastFrameData = dataObject;
            if (dataObject.DistanceFromPlayer >= 7.5f)
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
            Debug.Log("Executing charge");
            m_IsCharging = true;
            m_ChargeLocation = m_LastFrameData.PlayerLocation;
        }

        public void Stop()
        {
            Debug.Log("Stopping charge");
            m_IsCharging = false;
        }

        private void Charge()
        {
            m_CharacterMovement.Move(m_ChargeLocation - transform.position);
            transform.LookAt(m_ChargeLocation);
        }
    }
}
