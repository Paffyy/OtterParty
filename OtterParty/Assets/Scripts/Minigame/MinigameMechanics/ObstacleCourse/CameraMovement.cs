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
    [SerializeField]
    private bool miniGameActive;

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartCameraMovement);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopCameraMovement);
    }

    void FixedUpdate()
    {
        if (miniGameActive)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, endPosition.position, movingSpeed * Time.deltaTime);
        }
    }

    public void ApplySpeedMultplier(float speedMultiplier)
    {
        movingSpeed *= speedMultiplier;
    }

    private void StopCameraMovement(BaseEventInfo e)
    {
        miniGameActive = false;
    }

    private void StartCameraMovement(BaseEventInfo e)
    {
        miniGameActive = true;
    }
}
