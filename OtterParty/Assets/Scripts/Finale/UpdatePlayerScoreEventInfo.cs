using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerScoreEventInfo : BaseEventInfo
{
    public int Score { get; set; }
    public GameObject Player { get; set; }
}
