﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRubberDuckyController : MonoBehaviour
{
    [SerializeField] Transform rotatingObject;
    [SerializeField] float rotationModifier;
    public bool GameStarted { get; set; }
    private void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, SetGameStarted);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, SetGameEnded);

    }

    private void SetGameStarted(BaseEventInfo e)
    {
        GameStarted = true;
    }

    private void SetGameEnded(BaseEventInfo e)
    {
        GameStarted = false;
    }

    void Update()
    {
        if (GameStarted || true)
        {
            rotatingObject.Rotate(new Vector3(0, 10, 0) * rotationModifier * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other) // playarea
    {
        if (other.CompareTag("Player"))
        {
            EliminateEventInfo e = new EliminateEventInfo(other.gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, e);
        }
    }
}