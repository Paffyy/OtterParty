using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPointsToPlayer : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField]
    private Image playerImage;
    [SerializeField]
    private TextMeshProUGUI currentPointsText;
    [SerializeField]
    private TextMeshProUGUI pointsToAddText;
    [SerializeField]
    private List<Sprite> playerSprites;
    private Player player;

    public AddPointsToPlayer(Player player)
    {
        this.player = player;
    }
    private void Awake()
    {
        if (player != null)
        {
            playerImage.sprite = playerSprites[player.ID];
        }
        else
        {
            playerImage.sprite = playerSprites[0];
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            UpdatePoints();
        }
    }
    private void Start()
    {
        SetPoints();
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
    public void UpdateScore()
    {
        if (GameController.Instance != null)
            currentPointsText.text = GameController.Instance.PointSystem.GetCurrentScore()[player].ToString();
        else
            currentPointsText.text = "9";
    }
    private void UpdatePoints()
    {
        pointsToAddText.GetComponent<Animator>().SetTrigger("Start");
        // start Animation stuff points add to current
    }
}
