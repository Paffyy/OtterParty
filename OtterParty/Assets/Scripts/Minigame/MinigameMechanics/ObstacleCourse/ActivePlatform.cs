using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    protected void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent = parent;
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            player.Transition<OnMovingPlatformState>();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Transition<MovingState>();
        }
    }
}
