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
        if (GameController.Instance != null)
        {
            foreach (var item in GameController.Instance.Players)
            {
                item.PlayerObject.GetComponent<PlayerController>().PaintPoint.GetComponent<Painter>().Floor = paintFloor.GetComponent<MeshRenderer>();
            }
        }
        else
        {
            FindObjectOfType<Painter>().Floor = paintFloor.GetComponent<MeshRenderer>();
        }
    }

    private void StopGame(BaseEventInfo e)
    {
        var playerPercentages = paintFloor.GetComponent<CalculatePixelsScript>().GetPlayerPercentage();
        var playerScores = new Dictionary<Player, float>();
        foreach (var item in GameController.Instance.Players)
        {
            playerScores.Add(item, playerPercentages[item.ID]);
        }
        var sorted = from playerScore
                     in playerScores
                     orderby playerScore.Value
                     select playerScore;
        int placementsScore = 0;
        var pointSystem = MinigameController.Instance.MinigamePointSystem;
        foreach (var item in sorted.ToList())
        {
            pointSystem.GetCurrentScore()[item.Key] = placementsScore;
            placementsScore++;
        }
        MinigameController.Instance.PlayerPercentageScore = playerPercentages;
    }
}
