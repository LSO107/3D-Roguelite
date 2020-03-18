using UnityEngine;
using UnityEngine.AI;

namespace Character.Movement
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (CharacterMovement))]
    internal sealed class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent Agent { get; private set; }
        public CharacterMovement CharacterMovement { get; private set; }

        public Transform target;


        private void Start()
        {
            Agent = GetComponentInChildren<NavMeshAgent>();
            CharacterMovement = GetComponent<CharacterMovement>();

	        Agent.updateRotation = false;
	        Agent.updatePosition = true;
        }

        private void Update()
        {
            if (target != null)
                Agent.SetDestination(target.position);

            CharacterMovement.Move(Agent.remainingDistance > Agent.stoppingDistance ? Agent.desiredVelocity : Vector3.zero);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
