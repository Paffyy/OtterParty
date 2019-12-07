using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintToWinController : MonoBehaviour
{

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartGame);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopGame);
    }



    private void StartGame(BaseEventInfo e)
    {
        
    }

    private void StopGame(BaseEventInfo e)
    {

    }

    void Update()
    {
        
    }
}
