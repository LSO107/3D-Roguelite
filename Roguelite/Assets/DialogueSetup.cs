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
        ToggleCanvasGroups(1);
    }

    public void DisplayTwoOptions(DialogueOption optionOne, DialogueOption optionTwo)
    {
        m_TwoOptions.Setup(optionOne, optionTwo, i => m_NpcEngine.GoToNextState(i));
        ToggleCanvasGroups(2);
    }

    public void DisplayThreeOptions(DialogueOption optionOne, DialogueOption optionTwo, DialogueOption optionThree)
    {
        m_ThreeOptions.Setup(optionOne, optionTwo, optionThree, i => m_NpcEngine.GoToNextState(i));
        ToggleCanvasGroups(3);
    }

    private void ToggleCanvasGroups(int numberOfOptions)
    {
        switch (numberOfOptions)
        {
            case 3:
                m_ThreeOptionCanvasGroup.ToggleCanvasGroup(true);
                m_TwoOptionCanvasGroup.ToggleCanvasGroup(false);
                m_ContinueCanvasGroup.ToggleCanvasGroup(false);
                break;
            case 2:
                m_ThreeOptionCanvasGroup.ToggleCanvasGroup(false);
                m_TwoOptionCanvasGroup.ToggleCanvasGroup(true);
                m_ContinueCanvasGroup.ToggleCanvasGroup(false);
                break;
            default:
                m_ThreeOptionCanvasGroup.ToggleCanvasGroup(false);
                m_TwoOptionCanvasGroup.ToggleCanvasGroup(false);
                m_ContinueCanvasGroup.ToggleCanvasGroup(true);
                break;
        }
    }
}