using System;
using System.Collections.Generic;
using System.Linq;
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

    private readonly List<ScheduledEvent> m_ScheduledEvents = new List<ScheduledEvent>();

    private readonly List<Action> m_StartOfDayEvents = new List<Action>();
    private readonly List<Action> m_EndOfDayEvents = new List<Action>();

    public static DayNightCycle Instance;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        m_Sun = GetComponent<Light>();
        sunInitialIntensity = m_Sun.intensity;
        m_ScheduledEvents.Add(new ScheduledEvent(0.5f, () => Debug.Log("Scheduled Event Triggered")));
    }

    private void Update()
    {
        if (currentTimeOfDay >= 1)
        {
            UpdateSun();
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

    public void RegisterStartOfDayEvent(Action action)
    {
        m_StartOfDayEvents.Add(action);
    }

    public void RegisterEndOfDayEvent(Action action)
    {
        m_EndOfDayEvents.Add(action);
    }

    /// <summary>
    /// Reset time and scheduled events, run start of day events
    /// </summary>
    public void StartNewDay()
    {
        print("Starting new day");

        foreach (var startEvent in m_StartOfDayEvents)
        {
            startEvent();
        }

        foreach (var scheduledEvent in m_ScheduledEvents)
        {
            scheduledEvent.Reset();
        }

        currentTimeOfDay = 0f;
    }

    /// <summary>
    /// Run all end of day events and set time to 1
    /// </summary>
    public void EndDay()
    {
        foreach (var endEvent in m_EndOfDayEvents)
        {
            endEvent();
        }

        var remainingEvents = m_ScheduledEvents.Where(scheduledEvent => !scheduledEvent.HasRunToday);
        foreach (var scheduledEvent in remainingEvents)
        {
            scheduledEvent.InvokeAction();
        }

        currentTimeOfDay = 1f;

        GameManager.Instance.Blacksmith.GenerateShopItems();
    }

    /// <summary>
    /// Run all scheduled events that have not ran today
    /// </summary>
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
        //TODO: Fix rotation, shadows too long, looks weird
        var x = currentTimeOfDay * 360f - 90;
        transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

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