using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPointsToPlayer : MonoBehaviour
{
    [SerializeField]
    [Range(1,4)]
    private int placementUpdateDelay;
    [Header("Refrences")]
    [SerializeField]
    private List<Sprite> playerSprites;
    [SerializeField]
    private List<RectTransform> placements;
    [SerializeField]
    private PlayerScore playerScorePrefab;
    private List<PlayerScore> playerScores = new List<PlayerScore>();
    public bool IsPointsBased { get; set; }

    private void Start()
    {
        StartPointsUI();
    }
    private void StartPointsUI()
    {
        if (GameController.Instance != null && MinigameController.Instance != null)
        {
            PointSystem gameControllerPointsystem = new PointSystem();
            if (IsPointsBased)
            {
                gameControllerPointsystem.InitializePlayers(GameController.Instance.Players);
            }
            else
            {
                gameControllerPointsystem = GameController.Instance.PointSystem;
            }
            var minigameControllerPointSystem = MinigameController.Instance.MinigamePointSystem;
            foreach (var item in GameController.Instance.Players)
            {
                PlayerScore ps = Instantiate(playerScorePrefab, transform);
                playerScores.Add(ps);
                ps.Player = item;
                ps.PlayerImage.sprite = playerSprites[item.ID];
                ps.UpdatePoints(gameControllerPointsystem.GetCurrentScore()[item], minigameControllerPointSystem.GetCurrentScore()[item]);
            }
            UpdatePlayerPlacement();
            if (IsPointsBased)
            {

            }
            else
            {
                GameController.Instance.PointSystem.UpdateScore(minigameControllerPointSystem.GetCurrentScore());
            }
            StartCoroutine("UpdatePlacement");
        }
    }

    IEnumerator UpdatePlacement()
    {
        yield return new WaitForSeconds(placementUpdateDelay);
        UpdatePlayerPlacement();
    }

    private void UpdatePlayerPlacement()
    {
        Dictionary<Player, int> pointDictionary = new Dictionary<Player, int>();
        if (IsPointsBased)
        {
            pointDictionary = MinigameController.Instance.MinigamePointSystem.GetCurrentScore();
        }
        else
        {
            pointDictionary = GameController.Instance.PointSystem.GetCurrentScore();
        }
        var sorted = from playerScore
                     in pointDictionary
                     orderby - playerScore.Value
                     select playerScore;
        int placementOrder = 0;
        foreach (var item in sorted.ToList())
        {
            PlayerScore ps = playerScores.FirstOrDefault(x => x.Player == item.Key);
            ps.GetComponent<RectTransform>().anchoredPosition = placements[placementOrder].anchoredPosition;
            placementOrder++;
        }
    }
}
