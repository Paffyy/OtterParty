using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeedTrigger : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    private float speedMultiplier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killzone"))
        {
            GetComponentInParent<CameraMovement>().ApplySpeedMultplier(speedMultiplier);
        }
    }
}
