using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueDialogueSetup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Button m_Button;

    public void Setup(string text, Action clickAction)
    {
        m_Button.onClick.RemoveAllListeners();
        m_Text.text = text;
        m_Button.onClick.AddListener(() => clickAction());
    }
}
