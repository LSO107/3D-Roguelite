using System.Collections.Generic;
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
            PlayerManager.Instance.DisableInput();
            canvasGroup.ToggleCanvasGroup(true);
        }

        public static void CloseUserInterface(CanvasGroup canvasGroup)
        {
            PlayerManager.Instance.EnableInput();
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
