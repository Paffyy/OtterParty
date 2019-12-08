using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaintToWinController : MonoBehaviour
{
    [SerializeField]
    private GameObject paintFloor;

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
        var pointSystem = MinigameController.Instance.MinigamePointSystem;
        var playerPercentages = paintFloor.GetComponent<CalculatePixelsScript>().GetPlayerPercentage();
        pointSystem.InitializePlayers(GameController.Instance.Players);
        foreach (var item in pointSystem.GetCurrentScore())
        {

        }
        MinigameController.Instance.PlayerPercentageScore = paintFloor.GetComponent<CalculatePixelsScript>().GetPlayerPercentage();
    }

    void Update()
    {
        
    }
}
