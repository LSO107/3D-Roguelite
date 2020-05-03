using UnityEngine;

internal sealed class StartDialogue : MonoBehaviour
{
    [SerializeField] private int m_NpcId;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        NpcEngine.Instance.StartDialogue(m_NpcId);
    }
}
