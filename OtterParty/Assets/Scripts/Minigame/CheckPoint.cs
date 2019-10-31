using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Transform respawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckPointEventInfo eventInfo = new CheckPointEventInfo(other.gameObject, respawnLocation);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, eventInfo);
        }
    }
}
