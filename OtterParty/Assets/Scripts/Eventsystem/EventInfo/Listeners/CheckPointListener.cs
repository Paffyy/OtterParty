﻿using System.Collections;
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
    [SerializeField]
    private bool isCameraDependent;


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
        EventHandler.Instance.FireEvent(EventHandler.EventType.UpdateUIEvent, new UpdateUIEventInfo(player));
        if (!playerRespawns.ContainsKey(player))
        {
            playerRespawns.Add(player, 1);
            DetermineSpawnPoint(player);
        }
        else if (playerRespawns[player] < numberOfRespawns - 1)
        {
            playerRespawns[player]++;
            DetermineSpawnPoint(player);
        }
        else
        {
            EliminateEventInfo e = new EliminateEventInfo(player);
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, e);
        }
    }

    private void SpawnOnDefaultPosition(GameObject player)
    {
        player.GetComponent<PlayerController>().Transition<AirState>();
        player.transform.position = firstCheckPoint.position;
        player.transform.rotation = firstCheckPoint.rotation;
        player.GetComponent<PlayerController>().BodyCollider.enabled = true;
    }

    private void SpawnOnLastCheckPoint(GameObject player)
    {
        player.GetComponent<PlayerController>().Transition<AirState>();
        player.transform.position = playerCheckPoints[player].position;
        player.transform.rotation = playerCheckPoints[player].rotation;
        player.GetComponent<PlayerController>().BodyCollider.enabled = true;
    }

    private void SetPlayerCheckPoint(BaseEventInfo e)
    {
        CheckPointEventInfo eventInfo = e as CheckPointEventInfo;
        if(eventInfo != null)
        {
            if (isCameraDependent)
            {
                if (GameController.Instance != null)
                {
                    foreach (var player in GameController.Instance.Players)
                    {
                        playerCheckPoints[player.PlayerObject] = eventInfo.checkPoint;
                    }
                }
            }
            else
            {
                playerCheckPoints[eventInfo.playerObject] = eventInfo.checkPoint;
            }
        }
    }
}
