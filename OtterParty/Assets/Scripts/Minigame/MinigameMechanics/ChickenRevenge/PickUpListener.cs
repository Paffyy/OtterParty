using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpListener : BaseListener
{
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
        }
    }

}
