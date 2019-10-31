using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEventInfo eventInfo = new PlayerEventInfo(other.gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.RespawnEvent, eventInfo);
        }
    }
}
