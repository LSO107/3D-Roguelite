using System;
using ScriptingFramework;
using UnityEngine;
using UnityEngine.UI;

public class ThreeOptionDialogueSetup : MonoBehaviour
{
    [SerializeField] private Text m_OptionOne;
    [SerializeField] private Button m_ButtonOne;

    [SerializeField] private Text m_OptionTwo;
    [SerializeField] private Button m_ButtonTwo;
    
    [SerializeField] private Text m_OptionThree;
    [SerializeField] private Button m_ButtonThree;

    public void Setup(DialogueOption optionOne, DialogueOption optionTwo, DialogueOption optionThree, Action<int> onClick)
    {
        m_ButtonOne.onClick.RemoveAllListeners();
        m_ButtonTwo.onClick.RemoveAllListeners();
        m_ButtonThree.onClick.RemoveAllListeners();

        m_OptionOne.text = optionOne.Text;
        m_OptionTwo.text = optionTwo.Text;
        m_OptionThree.text = optionThree.Text;

        m_ButtonOne.onClick.AddListener(() => onClick(optionOne.NewState));
        m_ButtonTwo.onClick.AddListener(() => onClick(optionTwo.NewState));
        m_ButtonThree.onClick.AddListener(() => onClick(optionThree.NewState));
    }
}
