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
            m_Parent = GetComponent<EnemyMovementLogic>();
            m_Random = random;
            m_Movement = movement;
        }

        private void OnGUI()
        {
            GUILayout.Label($"Retreating: {m_IsRetreating}");
            GUILayout.Label($"Distance: {m_LastFrameData.DistanceFromPlayer}");
            GUILayout.Label($"Damage In Past 5 Secs: {m_LastFrameData.Health.DamageTaken}");
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

            if (m_LastFrameData.DistanceFromPlayer >= 10f)
            {
                m_Parent.UnregisterExecutionRequest(this);
                // Probably add a retreat timer too 
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
            Debug.Log("EXECUTE RETREAT.");
            m_IsRetreating = true;
            // Tell parent to stop combat
        }

        public void Stop()
        {
            Debug.Log("STOP RETREAT.");
            m_IsRetreating = false;
            // Tell parent we can do combat again
        }

        private void Retreat(Vector3 playerLocation)
        {
            Debug.Log("RETREATING NOW");
            var myPos = transform.position;
            var dir = myPos - playerLocation;
            m_Movement.Move(dir * 0.6f);
            transform.LookAt(myPos + dir);
        }
    }
}
