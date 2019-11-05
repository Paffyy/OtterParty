﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitListener : BaseListener
{
    [SerializeField]
    private bool hasLimitedLives;
    [SerializeField]
    [Range(1, 10)]
    private int numberOfLives;
    private Dictionary<GameObject, int> playerLives = new Dictionary<GameObject, int>();

    public override void Register()
    {
       // EventHandler.Instance.Register(EventHandler.EventType.HitEvent, ApplyShootEffects);
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, PlayerGotHit);
    }

    private void ApplyShootEffects(BaseEventInfo e)
    {
        var hitEventInfo = e as HitEventInfo;
        if (hitEventInfo != null)
        {
            Vector3 direction = Manager.Instance.GetFacingDirection(hitEventInfo.ObjectThatFired.transform, hitEventInfo.ObjectHit.transform);
            int knockbackMagnitude = Constants.Instance.DefaultKnockbackDistance;
            Vector3 knockbackVector =  Vector3.ProjectOnPlane(direction * knockbackMagnitude,Vector3.up);
            hitEventInfo.ObjectHit.GetComponent<Rigidbody>().velocity += -knockbackVector;
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent,new EliminateEventInfo(hitEventInfo.ObjectHit));
        }
    }

    private void PlayerGotHit(BaseEventInfo e)
    {
        var hitEventInfo = e as HitEventInfo;
        if(hitEventInfo != null)
        {
            GameObject playerThatShot = hitEventInfo.ObjectThatFired;
            GameObject playerThatGotHit = hitEventInfo.ObjectHit;
            if (hasLimitedLives)
            {
                CheckPlayerLives(playerThatGotHit);
            }
        }
    }

    private void CheckPlayerLives(GameObject playerHit)
    {
        if (!playerLives.ContainsKey(playerHit))
        {
            playerLives.Add(playerHit, 1);
        }
        else if(playerLives[playerHit] < numberOfLives)
        {
            playerLives[playerHit]++;
        }
        else
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, new EliminateEventInfo(playerHit));
        }
    }
}
