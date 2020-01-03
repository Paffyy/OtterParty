using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StunObstacle : MonoBehaviour
{
    private Collider coll;
    private void Awake()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Transition<LockedKnockbackState>();
            other.GetComponent<PlayerController>().PlayHitSound();
            Destroy(gameObject);
        }
    }
}
