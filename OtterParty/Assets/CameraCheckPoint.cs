using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheckPoint : MonoBehaviour
{
    [SerializeField]
    private Transform respawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Camera"))
        {
            CheckPointEventInfo eventInfo = new CheckPointEventInfo(other.gameObject, respawnLocation);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, eventInfo);
        }
    }
}
