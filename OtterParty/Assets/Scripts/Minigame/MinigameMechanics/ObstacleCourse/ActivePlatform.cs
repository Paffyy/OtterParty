using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    protected void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            other.gameObject.transform.parent = parent;
            player.Transition<OnMovingPlatformState>();
        }
    }

    protected void OnCollisionExit(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.Parent != null)
            {
                other.gameObject.transform.parent = player.Parent;
            }
            else
            {
                other.gameObject.transform.parent = null;
            }
            player.Transition<MovingState>();
        }
    }
}
