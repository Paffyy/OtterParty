using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPointBars : UIPlayerScore
{
    [Header("Refrences")]
    [SerializeField]
    private List<GameObject> playerBars;
    [SerializeField]
    private float countingSpeedMultiplier;

    public override void InitPointsUI()
    {
        if (GameController.Instance != null && MinigameController.Instance != null)
        {
            Dictionary<Player, int> finalScore = new Dictionary<Player, int>();
            foreach (var item in GameController.Instance.Players)
            {
                var playerBar = playerBars[item.ID].GetComponent<PlayerSliders>();
                playerBar.gameObject.SetActive(true);
                playerBar.Percentage = MinigameController.Instance.PlayerPercentageScore[item.ID];
                playerBar.SpeedMultiplier = countingSpeedMultiplier;
                playerBar.StartCountingPoints();
            }
        }
    }

    void Update()
    {
        
    }
}
