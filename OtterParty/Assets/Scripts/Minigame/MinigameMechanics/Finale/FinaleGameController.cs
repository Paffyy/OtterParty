using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinaleGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject pinataSpawner;

    void Start()
    {
        //EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartGame);
        StartGame(new StartMinigameEventInfo());
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopGame);
    }

    private void StopGame(BaseEventInfo e)
    {
        StopAllCoroutines();
    }

    private void StartGame(BaseEventInfo e)
    {
        var sortedList = SortByAscending(GameController.Instance.PointSystem.GetCurrentScore());
        PointSystem pointSystem = new PointSystem();
        pointSystem.InitializePlayers(GameController.Instance.Players);
        KeyValuePair<Player, int> previousScore = new KeyValuePair<Player, int>(new Player(), int.MinValue);
        int leadMultiplier = 1;
        foreach (var item in sortedList)
        {
            if (item.Value == previousScore.Value)
            {
                pointSystem.GetCurrentScore()[item.Key] = pointSystem.GetCurrentScore()[previousScore.Key];
            }
            else
            {
                pointSystem.GetCurrentScore()[item.Key] = leadMultiplier;
            }
            leadMultiplier++;
            previousScore = item;
        }
        pinataSpawner.GetComponent<FinalePinataSection>().InitPinataSection(pointSystem);
    }

    public List<KeyValuePair<Player, int>> SortByAscending(Dictionary<Player, int> pointDictionary)
    {
        var sorted = from playerScore
                     in pointDictionary
                     orderby -playerScore.Value
                     select playerScore;
        return sorted.ToList();
    }
}
