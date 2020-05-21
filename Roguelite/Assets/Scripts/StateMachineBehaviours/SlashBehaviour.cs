using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class SlashBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip m_SlashClip;
        [SerializeField] private float m_Delay = 0.3f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.Play(m_SlashClip, m_Delay);
        }
    }
}
