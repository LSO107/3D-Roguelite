using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEngine;

internal sealed class NpcManager : MonoBehaviour
{
    private DialogueTrigger[] m_Npcs;

    public static NpcManager Instance;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        m_Npcs = FindObjectsOfType<DialogueTrigger>();
    }

    public GameObject GetNpc(int npcId)
    {
        return m_Npcs.First(n => n.NpcId == npcId).gameObject;
    }

    public void TeleportNpc(GameObject npc, Vector3 location)
    {
        StartCoroutine(TeleportNpcWithDelay(npc, location, 0));
    }

    public void TeleportNpc(GameObject npc, Vector3 location, float delay)
    {
        StartCoroutine(TeleportNpcWithDelay(npc, location, delay));
    }

    private IEnumerator TeleportNpcWithDelay(GameObject npc, Vector3 location, float delay)
    {
        yield return new WaitForSeconds(delay);
        npc.transform.position = location;
    }
}
