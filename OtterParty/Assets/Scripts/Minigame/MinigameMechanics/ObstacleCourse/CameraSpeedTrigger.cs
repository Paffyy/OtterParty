using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeedTrigger : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    private float speedMultiplier;
    [SerializeField]
    private GameObject speedIncreaseNotification;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Camera"))
        {
            GetComponentInParent<CameraMovement>().ApplySpeedMultplier(speedMultiplier);
            speedIncreaseNotification.SetActive(true);
        }
    }
}
