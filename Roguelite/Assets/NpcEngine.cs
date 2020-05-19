using Character.Movement;
using Dialogue;
using Player;
using ScriptingFramework;
using UnityEngine;

internal sealed class NpcEngine : MonoBehaviour
{
    private DialogueReader m_DialogueReader;

    public static NpcEngine Instance;

    private NpcScript m_CurrentScript;

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
        {
            Debug.Log("NO NPC FOUND");
            return;
        }

        PlayerManager.Instance.GetComponent<CharacterUserInput>().IsFrozen = true;
        PlayerManager.Instance.GetComponent<PlayerController>().ToggleInputBlocked();
        Camera.main.GetComponent<CameraFollow>().LockCamera();

        m_CurrentScript = ScriptActivator.CreateScriptInstance<NpcScript>(npcType, npcId);

        Debug.Log("Current State: " + m_CurrentScript.State);
        m_CurrentScript.Execute();
    }

    public void GoToNextState(int state)
    {
        Debug.Log("Next State: " + state);
        m_CurrentScript.State = state;
        m_CurrentScript.Execute();
    }
}
