using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEventInfo : BaseEventInfo
{
    public GameObject playerObject { get; set; }
    public Transform checkPoint { get; set; }

    public CheckPointEventInfo(GameObject player, Transform passedCheckPoint)
    {
        playerObject = player;
        checkPoint = passedCheckPoint;
    }
}
