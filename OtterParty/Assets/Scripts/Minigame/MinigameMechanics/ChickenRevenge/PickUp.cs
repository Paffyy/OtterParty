using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private int points;
    public int Points { get { return points; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FirePickUpEvent(other.gameObject);
        }
    }

    private void FirePickUpEvent(GameObject player)
    {
        PickUpEventInfo pei = new PickUpEventInfo(gameObject, player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.PickUpEvent, pei);
    }
}
