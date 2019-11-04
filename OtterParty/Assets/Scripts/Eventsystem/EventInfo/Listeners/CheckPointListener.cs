using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointListener : BaseListener
{

    private Dictionary<GameObject, Transform> playerCheckPoints = new Dictionary<GameObject, Transform>();
    [SerializeField]
    private Transform firstCheckPoint;
    [SerializeField]
    [Range(0, 10)]
    private int numberOfRespawns;
    private Dictionary<GameObject, int> playerRespawns = new Dictionary<GameObject, int>();
    [SerializeField]
    private bool hasLimitedRespawns;

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
            GameObject playerObject = eventInfo.playerObject;
            if (!hasLimitedRespawns)
            {
                UnlimitedRespawn(playerObject);
            }
            else
            {
                LimitedRespawn(playerObject);
            }
        }
    }

    private void UnlimitedRespawn(GameObject player)
    {
        DetermineSpawnPoint(player);
    }

    private void DetermineSpawnPoint(GameObject player)
    {
        if (!playerCheckPoints.ContainsKey(player))
        {
            SpawnOnDefaultPosition(player);
        }
        else
        {
            SpawnOnLastCheckPoint(player);
        }
    }

    private void LimitedRespawn(GameObject player)
    {
        if (!playerRespawns.ContainsKey(player))
        {
            playerRespawns.Add(player, 1);
            DetermineSpawnPoint(player);
        }
        else if (playerRespawns[player] < numberOfRespawns)
        {
            playerRespawns[player]++;
            DetermineSpawnPoint(player);
        }
        else
        {
            //eliminate
            Debug.Log(player + "eliminated");
        }
    }

    private void SpawnOnDefaultPosition(GameObject player)
    {
        player.transform.position = firstCheckPoint.position;
        player.transform.rotation = firstCheckPoint.rotation;
        player.GetComponent<PlayerController>().PlayerBody.enabled = true;
    }

    private void SpawnOnLastCheckPoint(GameObject player)
    {
        player.transform.position = playerCheckPoints[player].position;
        player.transform.rotation = playerCheckPoints[player].rotation;
        player.GetComponent<PlayerController>().PlayerBody.enabled = true;
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
