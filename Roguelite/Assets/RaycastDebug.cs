using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastDebug : MonoBehaviour
{
    private GameObject onMouse = null;
    private Camera m_Camera;

    void Awake()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out var hit, 99999f))
        {
            onMouse = hit.transform.gameObject;
        }

        var b = GetMouseOverUI();

        onMouse = b.gameObject;
    }

    void OnGUI()
    {
        GUILayout.Label(onMouse.name);
    }

    public static RaycastResult GetMouseOverUI()
    {
        var res = GetEventSystemRaycastResults();

        return res.FirstOrDefault
            (curRaysastResult => curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"));
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
