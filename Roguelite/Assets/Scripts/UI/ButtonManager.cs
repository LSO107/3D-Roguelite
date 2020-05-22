using UnityEngine;
using UnityEngine.UI;
using Utils;

public sealed class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] m_Buttons;

    public void DisableButtons()
    {
        UserInterfaceUtils.DisableButtons(m_Buttons);
    }

    public void EnableButtons()
    {
        UserInterfaceUtils.EnableButtons(m_Buttons);
    }
}
