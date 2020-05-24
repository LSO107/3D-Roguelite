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

        public void Initialize(Random random, CharacterMovement movement)
        {
            m_Random = random;
            m_Movement = movement;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GetComponent<EnemyHealth>().Damage(10);
            }

            if (!m_IsRetreating)
                return;

            Retreat(m_LastFrameData.PlayerLocation);

            if (m_LastFrameData.DistanceFromPlayer >= 4)
            {
                m_Parent.UnregisterExecutionRequest(this);
                // Probably add a retreat timer too 
                m_Parent.StopCurrentRoutine();
                m_IsRetreating = false;
            }
        }

        public void ProcessData(AiDataObject dataObject)
        {
            var healthPercentage = (dataObject.Health.MaxHealth / 100) * 30;

            if (dataObject.Health.DamageTaken >= healthPercentage)
            {
                m_Parent.RegisterExecutionRequest(this);
            }
        }

        public void Execute()
        {
            Retreat(m_LastFrameData.PlayerLocation);
            // Tell parent to stop combat
        }

        public void Stop()
        {
            m_IsRetreating = false;
            // Tell parent we can do combat again
        }

        private void Retreat(Vector3 playerLocation)
        {
            m_IsRetreating = true;
            Debug.Log("RETREATING NOW");
            var myPos = transform.position;
            var dir = myPos - playerLocation;
            m_Movement.Move(dir * 0.6f);
            transform.LookAt(myPos + dir);
        }
    }
}
