using Dialogue;
using ScriptingFramework;
using UnityEngine;

internal sealed class NpcEngine : MonoBehaviour
{
    private DialogueReader m_DialogueReader;

    public static NpcEngine Instance;

    private Script m_CurrentScript;

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

        m_DialogueReader = GetComponent<DialogueReader>();
    }

    public void StartDialogue(int npcId)
    {
        var npcType = m_DialogueReader.GetNpcType(npcId);

        if (npcType == null)
            return;

        var playerManager = GameManager.Instance.PlayerManager;

        m_CurrentScript = ScriptActivator.CreateScriptInstance(npcType, playerManager, npcId);

        m_CurrentScript.Execute();
    }


}
