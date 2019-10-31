using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventInfo : BaseEventInfo
{
    public GameObject playerObject { get; set; }
    
    public PlayerEventInfo(GameObject player)
    {
        playerObject = player;
    }
}
