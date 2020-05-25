using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DialogueUI
{
    public class ContinueDialogueSetup : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private Button m_Button;
#pragma warning restore 0649

        public void Setup(string text, Action clickAction)
        {
            m_Button.onClick.RemoveAllListeners();
            m_Text.text = text;
            m_Button.onClick.AddListener(() => clickAction());
        }
    }
}
