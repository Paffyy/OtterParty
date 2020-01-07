using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShowFinalScore : UIPlayerScore
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
        if (GameController.Instance != null)
        {
            PointSystem gameControllerPointsystem = new PointSystem();
            gameControllerPointsystem = GameController.Instance.PointSystem;
            foreach (var item in GameController.Instance.Players)
            {
                PlayerScore ps = Instantiate(playerScorePrefab, transform);
                playerScores.Add(ps);
                ps.Player = item;
                ps.PlayerImage.sprite = playerSprites[item.ID];
                ps.UpdateFinalScore(gameControllerPointsystem.GetCurrentScore()[item]);
            }
            UpdatePlacements();
        }
    }

    public override void UpdatePlacements()
    {
        Dictionary<Player, int> pointDictionary = new Dictionary<Player, int>();
        pointDictionary = GameController.Instance.PointSystem.GetCurrentScore();
        int placementOrder = 0;
        foreach (var item in SortByAscending(pointDictionary))
        {
            PlayerScore ps = playerScores.FirstOrDefault(x => x.Player == item.Key);
            ps.GetComponent<RectTransform>().anchoredPosition = placements[placementOrder].anchoredPosition;
            placementOrder++;
        }
    }
}
