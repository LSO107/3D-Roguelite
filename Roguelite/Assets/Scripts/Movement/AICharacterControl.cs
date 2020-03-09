using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    internal sealed class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent Agent { get; private set; }
        public ThirdPersonCharacter Character { get; private set; }

        public Transform target;


        private void Start()
        {
            Agent = GetComponentInChildren<NavMeshAgent>();
            Character = GetComponent<ThirdPersonCharacter>();

	        Agent.updateRotation = false;
	        Agent.updatePosition = true;
        }

        private void Update()
        {
            if (target != null)
                Agent.SetDestination(target.position);

            Character.Move(Agent.remainingDistance > Agent.stoppingDistance ? Agent.desiredVelocity : Vector3.zero);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
