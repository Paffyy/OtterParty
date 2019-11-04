﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private int numberOfRespawns;
    private Dictionary<GameObject, int> playerRespawns = new Dictionary<GameObject, int>();
    [SerializeField]
    private bool hasLimitedRespawns;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            if (!hasLimitedRespawns)
            {
                fireRespawnEvent(other.gameObject);
            }
            else if (!playerRespawns.ContainsKey(other.gameObject))
            {
                fireRespawnEvent(other.gameObject);
                playerRespawns.Add(other.gameObject, 1);
            }
            else if (playerRespawns[other.gameObject] < numberOfRespawns)
            {
                fireRespawnEvent(other.gameObject);
                playerRespawns[other.gameObject]++;
            }
            else
            {
                Debug.Log("Eliminated");
                //eliminate
            }
        }
    }

    private void fireRespawnEvent(GameObject player)
    {
        player.GetComponent<PlayerController>().Transition<MovingState>();
        PlayerEventInfo eventInfo = new PlayerEventInfo(player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.RespawnEvent, eventInfo);
    }          
}
