using System;
using ScriptingFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class FourOptionDialogueSetup : MonoBehaviour
    {
        [SerializeField] private Text m_OptionOne;
        [SerializeField] private Button m_ButtonOne;

        [SerializeField] private Text m_OptionTwo;
        [SerializeField] private Button m_ButtonTwo;

        [SerializeField] private Text m_OptionThree;
        [SerializeField] private Button m_ButtonThree;

        [SerializeField] private Text m_OptionFour;
        [SerializeField] private Button m_ButtonFour;

        public void Setup(DialogueOption optionOne, DialogueOption optionTwo, DialogueOption optionThree, DialogueOption optionFour, Action<int> onClick)
        {
            m_ButtonOne.onClick.RemoveAllListeners();
            m_ButtonTwo.onClick.RemoveAllListeners();
            m_ButtonThree.onClick.RemoveAllListeners();
            m_ButtonFour.onClick.RemoveAllListeners();

            m_OptionOne.text = optionOne.Text;
            m_OptionTwo.text = optionTwo.Text;
            m_OptionThree.text = optionThree.Text;
            m_OptionFour.text = optionFour.Text;

            m_ButtonOne.onClick.AddListener(() => onClick(optionOne.NewState));
            m_ButtonTwo.onClick.AddListener(() => onClick(optionTwo.NewState));
            m_ButtonThree.onClick.AddListener(() => onClick(optionThree.NewState));
            m_ButtonFour.onClick.AddListener(() => onClick(optionFour.NewState));
        }
    }
}
