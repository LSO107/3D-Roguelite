using UnityEngine;

namespace Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void ToggleCanvasGroup(this CanvasGroup group, bool show)
        {
            group.alpha = show ? 1 : 0;
            group.blocksRaycasts = show;
            group.interactable = show;
        }
    }
}
