﻿using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = System.Random;

internal sealed class NpcFight : MonoBehaviour
{
    [SerializeField] private List<GameObject> npcPrefabs = null;

    [SerializeField] private List<GameObject> m_CurrentNpcs = null;

    private Random m_Random;

    private bool m_FightHasBegun;

    public static NpcFight Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        m_Random = new Random();
    }

    private void Update()
    {
        if (m_FightHasBegun)
        {
            if (m_CurrentNpcs.Count <= 0)
            {
                DayNightCycle.Instance.EndDay();
                m_FightHasBegun = false;
            }
        }
    }

    public void RemoveCurrentNpc(GameObject npc)
    {
        m_CurrentNpcs.Remove(npc);
    }

    public void BeginNpcFight(int numberOfEnemies)
    {
        m_FightHasBegun = true;
        RemoveCurrentEnemies();

        for (var i = 0; i < numberOfEnemies; i++)
        {
            var npc = GetRandomNpc();
            var newNpc = Instantiate(npc, GetRandomLocation(), Quaternion.identity);
            GameManager.Instance.InstantiatePuff(newNpc.transform.position);
            newNpc.transform.LookAt(PlayerManager.Instance.transform);
            m_CurrentNpcs.Add(newNpc);
        }

        //InstantiateCurrentNpcs();
    }

    private GameObject GetRandomNpc()
    {
        var random = m_Random.Next(0, npcPrefabs.Count);
        return npcPrefabs[random];
    }

    private Vector3 GetRandomLocation()
    {
        var randomX = m_Random.Next(-75, -60);
        var randomZ = m_Random.Next(-100, -85);
        return new Vector3(randomX, 0, randomZ);
    }

    private void RemoveCurrentEnemies()
    {
        if (m_CurrentNpcs.Count <= 0)
            return;

        foreach (var npc in m_CurrentNpcs)
        {
            GameManager.Instance.InstantiatePuff(npc.transform.position);
            Destroy(npc.gameObject);
        }

        m_CurrentNpcs.Clear();
    }
}