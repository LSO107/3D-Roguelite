using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class DayNightCycle : MonoBehaviour
{
    private Light m_Sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay;
    [HideInInspector]
    public float timeMultiplier = 1f;

    float sunInitialIntensity;

    private List<ScheduledEvent> m_ScheduledEvents;

    private void Awake()
    {
        m_Sun = GetComponent<Light>();
        m_ScheduledEvents = new List<ScheduledEvent>();
        sunInitialIntensity = m_Sun.intensity;

        m_ScheduledEvents.Add(new ScheduledEvent(0.5f, () => Debug.Log("Scheduled Event Triggered")));
    }

    private void Update()
    {
        if (currentTimeOfDay >= 1)
        {
            return;
        }

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        HandleScheduledEvents();
        UpdateSun();
    }

    /// <summary>
    /// Registers an event to run at a specified time cycle between 0 and 1
    /// </summary>
    public void RegisterScheduledEvent(float time, Action action)
    {
        if (time < 0 || time > 1)
            throw new ArgumentException("Time was not between 0 and 1");

        var scheduledEvent = new ScheduledEvent(time, action);
        m_ScheduledEvents.Add(scheduledEvent);
    }

    public void StartNewDay()
    {
        foreach (var scheduledEvent in m_ScheduledEvents)
        {
            scheduledEvent.Reset();
        }

        currentTimeOfDay = 0f;
    }

    private void HandleScheduledEvents()
    {
        foreach (var scheduledEvent in m_ScheduledEvents)
        {
            if (currentTimeOfDay >= scheduledEvent.Time && !scheduledEvent.HasRunToday)
            {
                scheduledEvent.InvokeAction();
            }
        }
    }

    private void UpdateSun()
    {
        //TODO: Fix rotation, shadows too long, looks retarded
        var x = currentTimeOfDay * 360f - 90;
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(x, 30, 270), 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        m_Sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    private class ScheduledEvent
    {
        public float Time { get; }
        private Action Action { get; }
        public bool HasRunToday { get; private set; }

        public ScheduledEvent(float time, Action action)
        {
            Time = time;
            Action = action;
        }

        public void InvokeAction()
        {
            Action();
            HasRunToday = true;
        }

        public void Reset()
        {
            HasRunToday = false;
        }
    }
}