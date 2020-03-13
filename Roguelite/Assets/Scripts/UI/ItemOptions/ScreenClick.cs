using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ItemOptions
{
    // Screen overlay to check when the player has clicked away from the InventoryUI or ContextMenu
    // Hierarchy ordering is important. Must be placed below InventoryUI and ContextMenu
    //
    internal sealed class ScreenClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ItemContextMenu itemContextMenu;

        private CanvasGroup m_CanvasGroup;

        private void Start()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_CanvasGroup.ToggleCanvasGroup(false);
            itemContextMenu.HideItemContextMenu();
        }
    }
}
