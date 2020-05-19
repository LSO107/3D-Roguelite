using ItemData;
using Player;
using Shops;
using UI.ItemOptions;
using UI.Tooltip;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    //public PlayerManager PlayerManager;
    public ItemContextMenu ItemContextMenu;
    public ItemDatabaseManager ItemDatabase;
    public Tooltip Tooltip;

    public static GameManager Instance;

    public BlacksmithGeneration m_Blacksmith;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            m_Blacksmith.GenerateShopItems();
        }
    }
}
