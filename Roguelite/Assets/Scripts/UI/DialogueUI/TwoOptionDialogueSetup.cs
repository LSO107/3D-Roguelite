using System;
using ScriptingFramework;
using UnityEngine;
using UnityEngine.UI;

public class TwoOptionDialogueSetup : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Text m_OptionOne;
    [SerializeField] private Button m_ButtonOne;
    [SerializeField] private Text m_OptionTwo;
    [SerializeField] private Button m_ButtonTwo;
#pragma warning restore 0649

    public void Setup(DialogueOption optionOne, DialogueOption optionTwo, Action<int> onClick)
    {
        m_ButtonOne.onClick.RemoveAllListeners();
        m_ButtonTwo.onClick.RemoveAllListeners();

        m_OptionOne.text = optionOne.Text;
        m_OptionTwo.text = optionTwo.Text;

        m_ButtonOne.onClick.AddListener(() => onClick(optionOne.NewState));
        m_ButtonTwo.onClick.AddListener(() => onClick(optionTwo.NewState));
    }
}
