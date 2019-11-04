using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyUpEventInfo : BaseEventInfo
{
    public GameObject PlayerObject { get; set; }
    public ReadyUpEventInfo(GameObject playerObject)
    {
        PlayerObject = playerObject;
    }
}
