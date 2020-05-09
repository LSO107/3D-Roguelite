using System;
using Extensions;
using ScriptingFramework;
using UnityEngine;

internal sealed class DialogueSetup : MonoBehaviour
{
    [SerializeField] private ThreeOptionDialogueSetup m_ThreeOptions;
    [SerializeField] private TwoOptionDialogueSetup m_TwoOptions;
    [SerializeField] private ContinueDialogueSetup m_Continue;

    [SerializeField] private CanvasGroup m_ThreeOptionCanvasGroup;
    [SerializeField] private CanvasGroup m_TwoOptionCanvasGroup;
    [SerializeField] private CanvasGroup m_ContinueCanvasGroup;

    [SerializeField] private CanvasGroup m_BlacksmithCanvasGroup;
    private CanvasGroup m_ActiveCanvasGroup;

    public static DialogueSetup Instance;

    private NpcEngine m_NpcEngine;
    
    private void Awake()
    {
        Instance = this;
        m_NpcEngine = GetComponent<NpcEngine>();
    }

    public void DisplayContinue(string line, int nextState)
    {
        m_Continue.Setup(line, () => m_NpcEngine.GoToNextState(nextState));
        EnableCanvasGroup(m_ContinueCanvasGroup);
    }

    public void DisplayTwoOptions(DialogueOption optionOne, DialogueOption optionTwo)
    {
        m_TwoOptions.Setup(optionOne, optionTwo, i => m_NpcEngine.GoToNextState(i));
        EnableCanvasGroup(m_TwoOptionCanvasGroup);
    }

    public void DisplayThreeOptions(DialogueOption optionOne, DialogueOption optionTwo, DialogueOption optionThree)
    {
        m_ThreeOptions.Setup(optionOne, optionTwo, optionThree, i => m_NpcEngine.GoToNextState(i));
        EnableCanvasGroup(m_ThreeOptionCanvasGroup);
    }

    public void OpenBlacksmithShop()
    {
        EnableCanvasGroup(m_BlacksmithCanvasGroup);
    }

    private void EnableCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.ToggleCanvasGroup(true);

        if (m_ActiveCanvasGroup != null) 
            m_ActiveCanvasGroup.ToggleCanvasGroup(false);

        m_ActiveCanvasGroup = canvasGroup;
    }
}