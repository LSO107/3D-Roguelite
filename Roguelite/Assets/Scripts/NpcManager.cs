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

    private void Start()
    {
        RegisterGuideEvents();
    }

    /// <summary>
    /// Returns the first GameObject found containing the npcId
    /// </summary>
    public NpcData GetNpc(int npcId)
    {
        return m_Npcs.First(n => n.GetNpcId == npcId).GetComponent<NpcData>();
    }

    /// <summary>
    /// Teleport the Npc gameObject to the location after the delay period
    /// </summary>
    public void TeleportNpc(NpcData npc, Vector3 location, float delay)
    {
        StartCoroutine(TeleportNpcWithDelay(npc, location, delay));
    }

    private static IEnumerator TeleportNpcWithDelay(NpcData npc, Vector3 location, float delay)
    {
        var gm = GameManager.Instance;

        gm.InstantiatePuff(npc.StartLocation);
        gm.InstantiatePuff(npc.CurrentLocation);

        yield return new WaitForSeconds(delay);
        npc.transform.position = location;
    }

    /// <summary>
    /// Register Guide events for start and end of day
    /// </summary>
    private void RegisterGuideEvents()
    {
        var npc = GetNpc(3);

        DayNightCycle.Instance.RegisterStartOfDayEvent(() => TeleportNpc(npc, npc.HiddenLocation, 0.75f));
        DayNightCycle.Instance.RegisterEndOfDayEvent(() => TeleportNpc(npc, npc.StartLocation, 0.75f));
    }
}
