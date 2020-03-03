using UnityEngine;
using UnityEngine.AI;

namespace Player.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    internal sealed class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent Agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter Character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            Agent = GetComponentInChildren<NavMeshAgent>();
            Character = GetComponent<ThirdPersonCharacter>();

	        Agent.updateRotation = false;
	        Agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                Agent.SetDestination(target.position);

            if (Agent.remainingDistance > Agent.stoppingDistance)
                Character.Move(Agent.desiredVelocity, false, false);
            else
                Character.Move(Vector3.zero, false, false);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
