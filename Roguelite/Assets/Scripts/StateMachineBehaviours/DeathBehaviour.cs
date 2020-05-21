using Player;
using UnityEngine;

internal sealed class DeathBehaviour : StateMachineBehaviour
{
    private readonly Vector3 m_PlayerStartLocation = new Vector3(-71.21f, 0.045f, -90f);

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerManager.Instance.transform.position = m_PlayerStartLocation;
        PlayerManager.Instance.Health.UpdateHealthBar();
    }
}
