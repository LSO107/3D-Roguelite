using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class BlockBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip m_BlockClip = null;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.PlayScheduled(m_BlockClip, 0.6);
        }
    }
}
