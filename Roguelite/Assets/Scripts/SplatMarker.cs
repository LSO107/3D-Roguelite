using System.Collections;
using UnityEngine;

internal sealed class SplatMarker : MonoBehaviour
{
    private SpriteRenderer m_CurrentImage;

    [SerializeField] private Sprite m_RedSplat;
    [SerializeField] private Sprite m_BlueSplat;

    private TextMesh m_Text;
    private Camera m_Cam;

    private bool m_IsSplatActive;

    private void Awake()
    {
        m_Cam = Camera.main;
        m_CurrentImage = GetComponent<SpriteRenderer>();
        m_Text = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        transform.LookAt(m_Cam.transform);
    }

    /// <summary>
    /// Displays splat marker with damage amount
    /// </summary>
    public void Show(int damage)
    {
        var sprite = damage <= 0 ? m_BlueSplat : m_RedSplat;

        StartCoroutine(ShowSplatMarker(damage, sprite));
    }

    /// <summary>
    /// Prevents running numerous coroutines simultaneously  
    /// </summary>
    private IEnumerator ShowSplatMarker(int damage, Sprite sprite)
    {
        yield return new WaitUntil(() => m_IsSplatActive == false);
        m_IsSplatActive = true;
        Show(sprite, damage);

        yield return new WaitForSeconds(0.5f);
        Hide();
        m_IsSplatActive = false;
    }

    private void Show(Sprite sprite, int damage)
    {
        m_CurrentImage.sprite = sprite;
        m_Text.text = damage.ToString();
    }

    private void Hide()
    {
        m_CurrentImage.sprite = null;
        m_Text.text = "";
    }
}
