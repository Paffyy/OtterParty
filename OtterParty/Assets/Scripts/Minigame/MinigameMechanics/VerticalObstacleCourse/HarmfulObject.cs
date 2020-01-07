using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().PlayHitSound();
            fireRespawnEvent(other.gameObject);
        }
    }

    private void fireRespawnEvent(GameObject player)
    {
        player.GetComponent<PlayerController>().Transition<MovingState>();
        PlayerEventInfo eventInfo = new PlayerEventInfo(player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.RespawnEvent, eventInfo);
    }
}
