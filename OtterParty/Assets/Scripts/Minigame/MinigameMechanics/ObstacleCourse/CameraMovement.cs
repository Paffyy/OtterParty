using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform endPosition;
    [SerializeField]
    [Range(1.0f, 8.0f)]
    private float movingSpeed;
    private bool minigameStarted;

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartCameraMovement);
    }

    void FixedUpdate()
    {
        if (minigameStarted)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, endPosition.position, movingSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player killed");
        }
    }

    private void StartCameraMovement(BaseEventInfo e)
    {
        minigameStarted = true;
    }
}
