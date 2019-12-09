using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpinningRubberDuckController : MonoBehaviour
{
    [SerializeField] Transform rotatingObject;
    [SerializeField] float rotationModifier;
    public bool GameStarted { get; set; }
    private void Start()
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
            rotatingObject.Rotate(new Vector3(0, 0, rotationModifier * Time.deltaTime));
        }
    }
 
    private void OnTriggerExit(Collider other) // playarea
    {
        if (other.CompareTag("player"))
        {
            EliminateEventInfo e = new EliminateEventInfo(other.gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, e);
        }
    }
}
