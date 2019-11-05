using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUIEventInfo : BaseEventInfo
{
    public GameObject playerObject { get; set; }

    public UpdateUIEventInfo(GameObject player)
    {
        playerObject = player;
    }
}
