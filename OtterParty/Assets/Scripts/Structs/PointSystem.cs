using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem 
{
    private Dictionary<Player, int> playerScores;

    public PointSystem()
    {
        playerScores = new Dictionary<Player, int>();

    }
    public void InitializePlayers(List<Player> players)
    {
        foreach (var item in players)
        {
            playerScores.Add(item, 0);
        }
    }
    public void UpdateScore(Dictionary<Player, int> pointsToAdd)
    {
        foreach (var item in pointsToAdd.Keys)
        {
            if (playerScores.ContainsKey(item))
            {
                playerScores[item] += pointsToAdd[item];
            }
        }
    }
    //Implemented in MinigameController
    public Dictionary<Player, float> GetFinalGameAdvantage()
    {
        Dictionary<Player, float> temp = new Dictionary<Player, float>();
        return temp;
    }
    public Dictionary<Player, int> GetCurrentScore() => playerScores;
}
