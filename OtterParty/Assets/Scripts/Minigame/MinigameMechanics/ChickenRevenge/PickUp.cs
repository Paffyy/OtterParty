﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private int points;
    [SerializeField]
    private AudioClip pickUpSound;
    public int Points { get { return points; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FirePickUpEvent(other.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void FirePickUpEvent(GameObject player)
    {
        SoundEventInfo sei = new SoundEventInfo(pickUpSound, 0.4f, 1);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        PickUpEventInfo pei = new PickUpEventInfo(gameObject, player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.PickUpEvent, pei);
    }
}
