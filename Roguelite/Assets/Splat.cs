using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class Splat : MonoBehaviour
{
    private SpriteRenderer m_CurrentImage;

    [SerializeField] private SpriteRenderer m_RedSplat;
    [SerializeField] private SpriteRenderer m_BlueSplat;

    private CanvasGroup m_CanvasGroup;
    private TextMesh m_Text;

    private bool m_IsVisible = false;

    private void Awake()
    {
        m_CurrentImage = GetComponent<SpriteRenderer>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_Text = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void Show(int damage)
    {
        StartCoroutine(Show(damage.ToString()));
    }

    private IEnumerator Show(string damage)
    {
        yield return new WaitUntil(() => m_IsVisible == false);
        m_IsVisible = true;
        m_CurrentImage.GetComponent<Renderer>().enabled = true;
        m_CurrentImage.sprite = damage == "0" ? m_BlueSplat.sprite : m_RedSplat.sprite;
        m_Text.text = damage;
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.5f);
        m_CurrentImage.GetComponent<Renderer>().enabled = false;
        m_Text.text = "";
        m_IsVisible = false;
    }
}
