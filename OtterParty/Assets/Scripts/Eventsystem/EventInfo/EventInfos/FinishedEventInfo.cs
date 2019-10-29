using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedEventInfo : BaseEventInfo
{
    public GameObject PlayerWhoFinished { get; set; }

    public FinishedEventInfo(GameObject player)
    {
        PlayerWhoFinished = player;
    }
}
