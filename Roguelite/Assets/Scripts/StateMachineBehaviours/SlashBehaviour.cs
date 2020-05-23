using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class SlashBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip slashClip = null;
        [SerializeField] private float delay = 0.3f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.Play(slashClip, delay);
        }
    }
}
