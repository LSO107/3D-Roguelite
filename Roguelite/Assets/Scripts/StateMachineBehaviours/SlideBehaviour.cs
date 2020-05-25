using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class SlideBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip slideClip = null;
        [SerializeField] private AudioClip attackClip = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.Play(slideClip);
            PlayerManager.Instance.SoundEffects.Play(attackClip, 0.6f);
        }
    }
}
