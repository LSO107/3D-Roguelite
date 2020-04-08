using UnityEngine;

internal sealed class TorchManager : MonoBehaviour
{
    [SerializeField] private DayNightCycle dayNightCycle;

    private ParticleSystem[] m_Torches;

    private void Start()
    {
        m_Torches = GetComponentsInChildren<ParticleSystem>();
        dayNightCycle.RegisterScheduledEvent(0.25f, () => ToggleLights(false));
        dayNightCycle.RegisterScheduledEvent(0.75f, () => ToggleLights(true));
    }

    private void ToggleLights(bool on)
    {
        foreach (var torch in m_Torches)
        {
            if (on)
            {
                torch.Play();
                torch.GetComponentInParent<AudioSource>().Play();
            }
            else
            {
                torch.Stop();
                torch.GetComponentInParent<AudioSource>().Stop();
            }
        }
    }
}
