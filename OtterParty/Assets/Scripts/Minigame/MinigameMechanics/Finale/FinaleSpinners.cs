using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleSpinners : MonoBehaviour
{
    [SerializeField] Transform rotatingObject;
    [SerializeField] [Range(-1, 1)] int rotatingDirection;
    [SerializeField] float rotatingSpeed;
    public bool GameStarted { get; set; }

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, SetGameStarted);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, SetGameEnded);
    }

    private void SetGameStarted(BaseEventInfo e)
    {
        GameStarted = true;
    }

    private void SetGameEnded(BaseEventInfo e)
    {
        GameStarted = false;
    }

    void Update()
    {
        if (GameStarted)
        {
            rotatingObject.Rotate(new Vector3(0, rotatingSpeed, 0) * rotatingDirection * Time.deltaTime);
        }
    }
}
