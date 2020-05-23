using System.Collections.Generic;
using Character.Movement;
using Extensions;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public static class UserInterfaceUtils
    {
        public static void OpenUserInterface(CanvasGroup canvasGroup)
        {
            PlayerManager.Instance.GetComponent<CharacterUserInput>().IsFrozen = true;
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(true);
            canvasGroup.ToggleCanvasGroup(true);
        }

        public static void CloseUserInterface(CanvasGroup canvasGroup)
        {
            PlayerManager.Instance.GetComponent<CharacterUserInput>().IsFrozen = false;
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(false);
            canvasGroup.ToggleCanvasGroup(false);
        }

        public static void DisableButtons(IEnumerable<Button> buttons)
        {
            foreach (var button in buttons)
            {
                button.enabled = false;
            }
        }

        public static void EnableButtons(IEnumerable<Button> buttons)
        {
            foreach (var button in buttons)
            {
                button.enabled = true;
            }
        }
    }
}
