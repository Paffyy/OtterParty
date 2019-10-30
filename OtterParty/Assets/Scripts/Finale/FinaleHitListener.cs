using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleHitListener : BaseListener
{
    [SerializeField]
    [Range(1, 5)]
    private int winCondition;
    private Dictionary<GameObject, int> playerScore = new Dictionary<GameObject, int>();

    public override void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, EnemyHit);
    }

    private void EnemyHit(BaseEventInfo e)
    {
        HitEventInfo eventInfo = e as HitEventInfo;
        GameObject playerThatShot = eventInfo.ObjectThatFired;
        GameObject hitObject = eventInfo.ObjectHit;
        hitObject.GetComponent<FinaleEnemy>().Respawn();
        if (!playerScore.ContainsKey(playerThatShot))
        {
            playerScore.Add(playerThatShot, 1);
        }
        else
        {
            playerScore[playerThatShot]++;
        }
        if(playerScore[playerThatShot] == winCondition)
        {
            EliminateEventInfo winnerEvent = new EliminateEventInfo(playerThatShot);
            EventHandler.Instance.FireEvent(EventHandler.EventType.FinaleWinEvent, winnerEvent);
        }
    }
}
