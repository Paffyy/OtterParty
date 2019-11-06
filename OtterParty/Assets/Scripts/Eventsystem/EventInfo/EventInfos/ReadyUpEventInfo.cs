using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyUpEventInfo : BaseEventInfo
{
    public int PlayerID{ get; set; }
    public ReadyUpEventInfo(int playerID)
    {
        PlayerID = playerID;
    }
}
