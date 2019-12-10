using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEventInfo : BaseEventInfo
{
    public GameObject PickedUpObject { get; set; }
    public GameObject PlayerThatPickedUp { get; set; }
    public PickUpEventInfo(GameObject pickUp, GameObject player)
    {
        PickedUpObject = pickUp;
        PlayerThatPickedUp = player;
    }
}
