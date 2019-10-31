using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            other.gameObject.GetComponent<PlayerController>().Transition<MovingState>();
        }
    }
}
