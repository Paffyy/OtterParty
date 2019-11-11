using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementTrigger : MonoBehaviour
{
    private List<Collider> hasAlreadyEntered = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (hasAlreadyEntered.Contains(other))
        {
            return;
        }
        hasAlreadyEntered.Add(other);
        if (other.gameObject.CompareTag("Player"))
        {
            
            var rgd = other.gameObject.GetComponent<Rigidbody>();
            rgd.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            other.gameObject.GetComponent<PlayerController>().Transition<MovingState>();
            rgd.velocity += new Vector3(0, 2, 4);
        }
    }
}
