using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitListener : BaseListener
{
    private Dictionary<GameObject, int> playerScore = new Dictionary<GameObject, int>();

    public override void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, EnemyHit);
    }

    private void EnemyHit(BaseEventInfo e)
    {
        HitEventInfo eventInfo = e as HitEventInfo;
        if (eventInfo != null && !eventInfo.ObjectHit.CompareTag("Player"))
        {
            GameObject playerThatShot = eventInfo.ObjectThatFired;
            GameObject hitObject = eventInfo.ObjectHit;
            if (!playerScore.ContainsKey(playerThatShot))
            {
                playerScore.Add(playerThatShot, 1);
            }
            else
            {
                playerScore[playerThatShot]++;
            }
            Player p = GameController.Instance.FindPlayerByGameObject(playerThatShot);
            var points = new Dictionary<Player, int>();
            points.Add(p, 1);
            MinigameController.Instance.MinigamePointSystem.UpdateScore(points);
            Destroy(hitObject);
            UpdatePlayerScoreEventInfo updateEventInfo = new UpdatePlayerScoreEventInfo() { Player = playerThatShot, Score = playerScore[playerThatShot] };
            EventHandler.Instance.FireEvent(EventHandler.EventType.UpdateScoreEvent, updateEventInfo);
        }
    }
}
