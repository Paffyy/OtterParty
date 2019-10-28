﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    protected void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            other.gameObject.transform.parent = parent;
            player.Transition<OnMovingPlatformState>();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            other.gameObject.transform.parent = null;
            player.Transition<MovingState>();
        }
    }
}