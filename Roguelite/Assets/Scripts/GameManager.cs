﻿using Health;
using Items.Inventory;
using Player;
using UI.Inventory;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    //public GameObject RespawnLocation;

    public HealthObject PlayerHealth;
    public PlayerManager PlayerManager;
    public InventoryUI InventoryUI;

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