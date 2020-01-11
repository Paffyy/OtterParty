﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRubberDuckyController : MonoBehaviour
{
    [SerializeField] Transform rotatingObject;
    [SerializeField] float rotationOverTimeModifier;
    public bool GameStarted { get; set; }

    private void Awake()
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
        if (GameStarted)
        {
            rotatingObject.Rotate(new Vector3(0, 10, 0) * rotationOverTimeModifier * ImportManager.Instance.Settings.RotationMultiplier * Time.deltaTime);
            rotationOverTimeModifier += Time.deltaTime / 5;
        }
    }

    private void OnTriggerExit(Collider other) // playarea
    {
        if (GameStarted)
        {
            if (other.CompareTag("Player"))
            {
                EliminateEventInfo e = new EliminateEventInfo(other.gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, e);
            }
        }
    }
}