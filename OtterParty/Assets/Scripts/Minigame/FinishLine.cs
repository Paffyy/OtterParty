using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FinishedEventInfo eventInfo = new FinishedEventInfo(other.gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.FinishLineEvent, eventInfo);
        }
    }
}
