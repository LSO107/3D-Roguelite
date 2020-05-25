using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    internal sealed class DeathBehaviour : StateMachineBehaviour
    {
        private readonly Vector3 m_PlayerStartLocation = new Vector3(-71.21f, 0.045f, -90f);

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            NpcFight.Instance.EndCurrentFight();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var transform = PlayerManager.Instance.transform;
            transform.position = m_PlayerStartLocation;
            GameManager.Instance.InstantiatePuff(transform.position);
            PlayerManager.Instance.Health.UpdateHealthBar();
            DayNightCycle.Instance.EndDay();
        }
    }
}
