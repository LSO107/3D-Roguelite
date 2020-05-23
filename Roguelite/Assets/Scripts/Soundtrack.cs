using System.Collections;
using UnityEngine;

internal sealed class Soundtrack : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private AudioClip m_DaySoundTrack;
    [SerializeField] private AudioClip m_NightSoundTrack;
#pragma warning disable 0649

    private AudioSource m_AudioSource;

    private float m_FadeDuration = 1.25f;
    private bool m_FadeInProgress;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        DayNightCycle.Instance.RegisterStartOfDayEvent(() => ChangeClip(m_DaySoundTrack));
        DayNightCycle.Instance.RegisterEndOfDayEvent(() => ChangeClip(m_NightSoundTrack));
    }

    public void ChangeClip(AudioClip audioClip)
    {
        StartCoroutine(ProcessClipChange(audioClip));
    }

    /// <summary>
    /// Fade out any clip playing and fade in new audio clip
    /// </summary>
    private IEnumerator ProcessClipChange(AudioClip clip)
    {
        if (m_AudioSource.isPlaying)
        {
            StartCoroutine(FadeClip(0));
            yield return new WaitUntil(() => !m_FadeInProgress);
        }

        m_AudioSource.clip = clip;

        StartCoroutine(FadeClip(1));
        yield return new WaitUntil(() => !m_FadeInProgress);

        if (!m_AudioSource.isPlaying)
            m_AudioSource.Play();
    }

    /// <summary>
    /// Fade audio clip from current volume to target volume
    /// </summary>
    private IEnumerator FadeClip(float targetVolume)
    {
        m_FadeInProgress = true;
        float currentTime = 0;
        var start = m_AudioSource.volume;

        while (currentTime < m_FadeDuration)
        {
            currentTime += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / m_FadeDuration);
            yield return null;
        }

        m_FadeInProgress = false;
    }
}
