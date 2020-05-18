using UnityEngine;

internal sealed class Torch : MonoBehaviour
{
    private ParticleSystem m_Torch;
    private AudioSource m_Audio;

    [SerializeField] private DayNightCycle m_DayNightCycle;

    private void Start()
    {
        m_Torch = GetComponentInChildren<ParticleSystem>();
        m_Audio = GetComponentInChildren<AudioSource>();

        RegisterStartOfDayEvents();
        RegisterEndOfDayEvents();
    }

    private void RegisterStartOfDayEvents()
    {
        m_DayNightCycle.RegisterEndOfDayEvent(() => m_Torch.Stop());
        m_DayNightCycle.RegisterEndOfDayEvent(() => m_Audio.Stop());
    }

    private void RegisterEndOfDayEvents()
    {
        m_DayNightCycle.RegisterStartOfDayEvent(() => m_Torch.Play());
        m_DayNightCycle.RegisterStartOfDayEvent(() => m_Audio.Play());
    }
}
