using Player;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class SpinBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioClip spinClip = null;
        [SerializeField] private float delay = 0.3f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SoundEffects.Play(spinClip, delay);
        }
    }
}
