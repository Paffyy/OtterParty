using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointListener : BaseListener
{

    private Dictionary<GameObject, Transform> playerCheckPoints = new Dictionary<GameObject, Transform>();
    [SerializeField]
    private Transform firstCheckPoint;

    public override void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.RespawnEvent, RespawnPlayer);
        EventHandler.Instance.Register(EventHandler.EventType.CheckPointEvent, SetPlayerCheckPoint);
    }

    private void RespawnPlayer(BaseEventInfo e)
    {
        PlayerEventInfo eventInfo = e as PlayerEventInfo;
        if (eventInfo != null)
        {
            eventInfo.playerObject.GetComponent<BoxCollider>().enabled = true;
            if (!playerCheckPoints.ContainsKey(eventInfo.playerObject))
            {
                eventInfo.playerObject.transform.position = firstCheckPoint.position;
                eventInfo.playerObject.transform.rotation = firstCheckPoint.rotation;
            } else
            {
                eventInfo.playerObject.transform.position = playerCheckPoints[eventInfo.playerObject].position;
                eventInfo.playerObject.transform.rotation = playerCheckPoints[eventInfo.playerObject].rotation;
            }
        }
    }

    private void SetPlayerCheckPoint(BaseEventInfo e)
    {
        CheckPointEventInfo eventInfo = e as CheckPointEventInfo;
        if (eventInfo != null)
        {
            playerCheckPoints[eventInfo.playerObject] = eventInfo.checkPoint;
        }
    }
}
