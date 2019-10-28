using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddPointsToPlayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentPointsText;
    [SerializeField]
    private TextMeshProUGUI pointsToAddText;
    private Player player;

    public AddPointsToPlayer(Player player)
    {
        this.player = player;
    }

    public void UpdateScore()
    {
        if (GameController.Instance != null)
            currentPointsText.text = GameController.Instance.PointSystem.GetCurrentScore()[player].ToString();
        else
            currentPointsText.text = "9";
    }

    private void Start()
    {
        SetPoints();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            UpdatePoints();
        }
    }
    private void SetPoints()
    {
        if (GameController.Instance != null)
        {
            var gameControllerPointsystem = GameController.Instance.PointSystem;
            currentPointsText.text = gameControllerPointsystem.GetCurrentScore()[player].ToString();
            if (MinigameController.Instance != null)
            {
                var minigameControllerPointDictionary = MinigameController.Instance.MinigamePointSystem.GetCurrentScore();
                pointsToAddText.text = minigameControllerPointDictionary[player].ToString();
                gameControllerPointsystem.UpdateScore(minigameControllerPointDictionary);
            }
        }
    }
    private void UpdatePoints()
    {
        pointsToAddText.GetComponent<Animator>().SetTrigger("Start");
        // start Animation stuff points add to current
    }
}
