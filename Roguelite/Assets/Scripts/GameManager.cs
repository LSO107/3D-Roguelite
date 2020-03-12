using ItemData;
using Player;
using UI.ItemContextMenu;
using UI.Tooltip;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    //public GameObject RespawnLocation;

    public PlayerManager PlayerManager;
    public ItemContextMenu ItemContextMenu;
    public ItemDatabaseManager ItemDatabase;
    public Tooltip Tooltip;

    public static GameManager Instance;

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
}
