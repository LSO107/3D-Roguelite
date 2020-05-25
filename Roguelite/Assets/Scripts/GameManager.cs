using System.Collections;
using Dialogue;
using ItemData;
using Shops;
using UI.ItemOptions;
using UI.Tooltip;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    public GameObject PuffEffect;
    public CanvasGroup ScreenToFade;
    public BlacksmithGeneration Blacksmith;
    public ItemContextMenu ItemContextMenu;
    public ItemDatabaseManager ItemDatabase;
    public Tooltip Tooltip;
    public DialogueTrigger[] Npcs;

    private bool m_FadeInProgress;

    public static GameManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Npcs = FindObjectsOfType<DialogueTrigger>();
    }

    public void InstantiatePuff(Vector3 location)
    {
        Instantiate(PuffEffect, location + Vector3.up, Quaternion.identity);
    }

    public void FadeScreen()
    {
        if (m_FadeInProgress)
            return;

        StartCoroutine(ProcessFadingScreen());
    }

    private IEnumerator ProcessFadingScreen()
    {
        StartCoroutine(Fade(1, 1f));
        yield return new WaitUntil(() => !m_FadeInProgress);

        StartCoroutine(Fade(0, 3f));
        yield return new WaitUntil(() => !m_FadeInProgress);
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        m_FadeInProgress = true;
        float currentTime = 0;
        var start = ScreenToFade.alpha;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            ScreenToFade.alpha = Mathf.Lerp(start, targetAlpha, currentTime / duration);
            yield return null;
        }

        m_FadeInProgress = false;
    }
}
