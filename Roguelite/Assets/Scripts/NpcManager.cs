using System.Collections;
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

    /// <summary>
    /// Returns the first GameObject found containing the npcId
    /// </summary>
    public GameObject GetNpc(int npcId)
    {
        return m_Npcs.First(n => n.NpcId == npcId).gameObject;
    }

    /// <summary>
    /// Teleport the Npc gameObject to the location
    /// </summary>
    public void TeleportNpc(GameObject npc, Vector3 location)
    {
        StartCoroutine(TeleportNpcWithDelay(npc, location, 0));
    }

    /// <summary>
    /// Teleport the Npc gameObject to the location after the delay period
    /// </summary>
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
