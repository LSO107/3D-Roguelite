using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class ImpactBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip m_ImpactClip;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.Play(m_ImpactClip);
        }
    }
}
