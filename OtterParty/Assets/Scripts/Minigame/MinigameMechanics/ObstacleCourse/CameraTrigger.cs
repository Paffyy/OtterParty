using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Camera hit player");
            other.gameObject.GetComponent<PlayerController>().PlayerBody.enabled = false;
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
