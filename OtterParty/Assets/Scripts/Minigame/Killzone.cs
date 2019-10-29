using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EliminateEventInfo eventInfo = new EliminateEventInfo(other.gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, eventInfo);
        }
    }
}
