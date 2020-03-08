using Extensions;
using UnityEngine;
using UnityEngine.UI;

internal sealed class TestingScript : MonoBehaviour
{
    [SerializeField] private Button m_DamageHealthButton;
    [SerializeField] private Button m_HealHealthButton;
    [SerializeField] private Button m_ExperienceButton;
    [SerializeField] private Button m_EquipmentButton;


    public void Damage()
    {
        GameManager.Instance.PlayerManager.Health.Damage(10);
    }

    public void Heal()
    {
        GameManager.Instance.PlayerManager.Health.Heal(10);
    }

    public void AddExperience()
    {
        GameManager.Instance.PlayerManager.Experience.IncreaseExperience(10);
    }

    public void ToggleEquipment()
    {
        var equipment = GameManager.Instance.PlayerManager.EquipmentUI;
        var canvasGroup = equipment.GetComponent<CanvasGroup>();
        canvasGroup.ToggleCanvasGroup(!canvasGroup.interactable);
    }
}
