using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateEventInfo : BaseEventInfo
{
    public GameObject PlayerToEliminate { get; set; }

    public EliminateEventInfo(GameObject player)
    {
        PlayerToEliminate = player;
    }
}
