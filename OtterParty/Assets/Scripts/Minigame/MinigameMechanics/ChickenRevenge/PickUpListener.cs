using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpListener : BaseListener
{
    private Dictionary<GameObject, int> playerScore = new Dictionary<GameObject, int>();
    public override void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, RegisterPickUp);
    }

    private void RegisterPickUp(BaseEventInfo e)
    {
        PickUpEventInfo pei = e as PickUpEventInfo;
        if(pei != null)
        {
            GameObject pickUp = pei.PickedUpObject;
            GameObject player = pei.PlayerThatPickedUp;
            int pts = pickUp.GetComponent<PickUp>().Points;
            AssignPoints(player, pts);
            Player p = GameController.Instance.FindPlayerByGameObject(player);
            var points = new Dictionary<Player, int>();
            points.Add(p, pts);
            MinigameController.Instance.MinigamePointSystem.UpdateScore(points);
            UpdatePlayerScoreEventInfo updateEventInfo = new UpdatePlayerScoreEventInfo() { Player = player, Score = playerScore[player] };
            EventHandler.Instance.FireEvent(EventHandler.EventType.UpdateScoreEvent, updateEventInfo);
        }
    }

    private void AssignPoints(GameObject player, int points)
    {
        if (!playerScore.ContainsKey(player))
        {
            playerScore.Add(player, points);
        }
        else
        {
            playerScore[player] += points;
        }
    }
}
