using Extensions;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI.Settings
{
    internal sealed class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CanvasGroup m_PauseMenu = null;

        private PlayerController m_Player;

        private void Start()
        {
            m_Player = PlayerManager.Instance.GetComponent<PlayerController>();
        }

        public void OpenPauseMenu()
        {
            Time.timeScale = 0;
            PlayerManager.Instance.DisableInput();
            m_PauseMenu.ToggleCanvasGroup(true);
        }

        public void ClosePauseMenu()
        {
            Time.timeScale = 1;
            m_PauseMenu.ToggleCanvasGroup(false);
            PlayerManager.Instance.EnableInput();
        }

        public void Logout()
        {
            SceneManager.LoadScene("LoginScreen");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_Player.ToggleIsInputBlocked(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_PauseMenu.interactable)
                return;

            m_Player.ToggleIsInputBlocked(false);
        }
    }
}
