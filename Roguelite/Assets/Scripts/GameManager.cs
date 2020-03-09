using Player;
using UI.Tooltip;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    //public GameObject RespawnLocation;

    public PlayerManager PlayerManager;
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
