using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Settings
{
    internal sealed class EffectsVolume : MonoBehaviour
    {
        [SerializeField] private AudioMixer m_Mixer;

        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            m_Slider.value = PlayerPrefs.GetFloat("EffectsVolume", -30);
            m_Mixer.SetFloat("SFX", m_Slider.value);

            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Toggle the music volume on or off 
        /// </summary>
        public void ValueChangeCheck()
        {
            m_Mixer.SetFloat("SFX", m_Slider.value);
            PlayerPrefs.SetFloat("EffectsVolume", m_Slider.value);
            PlayerPrefs.Save();
        }
    }
}
