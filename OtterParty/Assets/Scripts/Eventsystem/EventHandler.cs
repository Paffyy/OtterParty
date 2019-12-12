using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void EventListener(BaseEventInfo e);
    public enum EventType {
        HitEvent,
        EliminateEvent,
        FinishLineEvent,
        StartMinigameEvent,
        FinaleWinEvent,
        RespawnEvent,
        CheckPointEvent,
        UpdateScoreEvent,
        TransitionEvent,
        ReadyUpEvent,
        StartReadyUpSequence,
        EndMinigameEvent,
        UpdateUIEvent,
        SetDistanceEvent,
        InstantiatedUIEvent,
        ParticleEvent,
        MultipleEliminateEvent,
        SoundEvent,
        StartNextRoundEvent,
        PickUpEvent,
        StartDNFtimerEvent
    }
    private Dictionary<EventType, List<EventListener>> eventListeners;
    private static EventHandler instance;
    public static EventHandler Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<EventHandler>();
            return instance;
        }
    }

    public void Register(EventType type, EventListener e)
    {
        if (!eventListeners.ContainsKey(type))
        {
            eventListeners.Add(type, new List<EventListener> { e });
        }
        else
        {
            var listeners = eventListeners[type];
            if (listeners == null)
                listeners = new List<EventListener>();
            listeners.Add(e);
        }
    }
    public void Unregister(EventType type, EventListener e)
    {
        if (!eventListeners.ContainsKey(type))
            return;
        var listeners = eventListeners[type];
        if (listeners == null)
            return;
        else if (listeners.Contains(e))
            listeners.Remove(e);
    }
    public void FireEvent(EventType type, BaseEventInfo e)
    {
        if (eventListeners == null || !eventListeners.ContainsKey(type) || eventListeners[type] == null)
        return;
        for (int i = eventListeners[type].Count - 1; i >= 0; i--)
        {
            eventListeners[type][i](e);
        }
    }

    void Awake()
    {
        eventListeners = new Dictionary<EventType, List<EventListener>>();
    }
}
