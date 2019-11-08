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
            int pts = 1;
            if (hitObject.GetComponent<PinataBehaviour>() != null)
            {
                Debug.Log("Pinata");
                pts = hitObject.GetComponent<PinataBehaviour>().Points;
            }
            AssignPoints(playerThatShot, pts);
            Player p = GameController.Instance.FindPlayerByGameObject(playerThatShot);
            var points = new Dictionary<Player, int>();
            points.Add(p, pts);
            MinigameController.Instance.MinigamePointSystem.UpdateScore(points);
            Destroy(hitObject);
            UpdatePlayerScoreEventInfo updateEventInfo = new UpdatePlayerScoreEventInfo() { Player = playerThatShot, Score = playerScore[playerThatShot] };
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
