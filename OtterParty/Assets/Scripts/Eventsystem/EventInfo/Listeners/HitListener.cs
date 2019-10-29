﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitListener : BaseListener
{
    private int _debug_lives = 3;
    public override void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, ApplyShootEffects);
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
}
