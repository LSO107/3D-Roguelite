using UnityEngine;

namespace Player
{
    internal sealed class SoundEffectsManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_AudioSource = null;

        public void Play(AudioClip clip)
        {
            m_AudioSource.PlayOneShot(clip);
        }

        public void Play(AudioClip clip, float delay)
        {
            m_AudioSource.clip = clip;
            m_AudioSource.PlayDelayed(delay);
        }

        public void PlayScheduled(AudioClip clip, double scheduledTime)
        {
            m_AudioSource.clip = clip;
            m_AudioSource.PlayScheduled(scheduledTime);
        }
    }
}
