﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private List<GameObject> triggeredObjects = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggeredObjects.Contains(other.gameObject))
            {
                triggeredObjects.Add(other.gameObject);
                FinishedEventInfo eventInfo = new FinishedEventInfo(other.gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.FinishLineEvent, eventInfo);
            }
        }
    }
}
