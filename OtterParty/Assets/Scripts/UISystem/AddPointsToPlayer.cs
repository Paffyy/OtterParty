using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddPointsToPlayer : UIPlayerScore
{
    [Header("Refrences")]
    [SerializeField]
    private List<Sprite> playerSprites;
    [SerializeField]
    private List<RectTransform> placements;
    [SerializeField]
    private PlayerScore playerScorePrefab;
    private List<PlayerScore> playerScores = new List<PlayerScore>();

    public override void InitPointsUI()
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
            UpdatePlacements();
            if (!IsPointsBased)
            {
                GameController.Instance.PointSystem.UpdateScore(minigameControllerPointSystem.GetCurrentScore());
            }
            StartCoroutine("UpdatePlacement");
        }
    }

    public override void UpdatePlacements()
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
        int placementOrder = 0;
        foreach (var item in SortByAscending(pointDictionary))
        {
            PlayerScore ps = playerScores.FirstOrDefault(x => x.Player == item.Key);
            ps.GetComponent<RectTransform>().anchoredPosition = placements[placementOrder].anchoredPosition;
            placementOrder++;
        }
    }
}
