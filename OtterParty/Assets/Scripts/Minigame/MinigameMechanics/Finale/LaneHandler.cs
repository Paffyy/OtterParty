using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneHandler : MonoBehaviour
{
    [SerializeField]
    private BoxCollider laneTrigger;
    [SerializeField]
    private Transform lanePosition;
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private GameObject gateAnimation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().PlayerState != PlayerController.CurrentPlayerState.LockedMovementState)
        {
            laneTrigger.enabled = false;
            gate.SetActive(true);
            gateAnimation.GetComponent<Animator>().SetTrigger("CloseGate");
            PlayerController p = other.gameObject.GetComponent<PlayerController>();
            var rgd = other.gameObject.GetComponent<Rigidbody>();
            p.Transition<LockedMovementState>();
            rgd.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;
            other.gameObject.transform.position = lanePosition.position;
            other.gameObject.transform.rotation = lanePosition.rotation;
            p.InputDirection = Vector2.zero;
            Destroy(this.gameObject);
        }
    }
}
