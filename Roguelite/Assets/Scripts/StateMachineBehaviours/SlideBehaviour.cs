using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class SlideBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip slideClip = null;
        [SerializeField] private float slideDelay = 0.3f;
        [SerializeField] private AudioClip attackClip = null;
        [SerializeField] private float attackDelay = 0.3f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.Play(slideClip);
            PlayerManager.Instance.SoundEffects.Play(attackClip, 0.6f);
        }
    }
}
