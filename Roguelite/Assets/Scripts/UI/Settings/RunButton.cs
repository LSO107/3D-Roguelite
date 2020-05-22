using Character.Movement;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Settings
{
    internal sealed class RunButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private PlayerController m_Player;
        private CharacterUserInput m_Input;
        private Button m_Button;
        private Color m_ButtonColour;

        private void Start()
        {
            m_Player = PlayerManager.Instance.GetComponent<PlayerController>();
            m_Input = PlayerManager.Instance.GetComponent<CharacterUserInput>();
            m_Button = GetComponent<Button>();
            m_ButtonColour = m_Button.image.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_Player.ToggleIsInputBlocked(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_Player.ToggleIsInputBlocked(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_Input.ToggleRun();
            m_Button.image.color = m_Input.IsRunning ? Color.green : m_ButtonColour;
        }
    }
}
