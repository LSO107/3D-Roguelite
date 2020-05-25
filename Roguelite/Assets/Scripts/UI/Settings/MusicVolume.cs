using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Settings
{
    internal sealed class MusicVolume : MonoBehaviour
    {
        [SerializeField] private AudioMixer m_Mixer = null;

        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            m_Slider.value = PlayerPrefs.GetFloat("MusicVolume", -30f);
            m_Mixer.SetFloat("Music", m_Slider.value);

            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Toggle the music volume on or off 
        /// </summary>
        public void ValueChangeCheck()
        {
            m_Mixer.SetFloat("Music", m_Slider.value);
            PlayerPrefs.SetFloat("MusicVolume", m_Slider.value);
            PlayerPrefs.Save();
        }
    }
}
