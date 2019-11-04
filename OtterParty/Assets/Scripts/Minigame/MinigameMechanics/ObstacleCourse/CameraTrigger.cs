﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().PlayerBody.enabled = false;
            fireRespawnEvent(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            other.gameObject.SetActive(false);
        }
    }

    private void fireRespawnEvent(GameObject player)
    {
        player.GetComponent<PlayerController>().Transition<MovingState>();
        PlayerEventInfo eventInfo = new PlayerEventInfo(player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.RespawnEvent, eventInfo);
    }
}
