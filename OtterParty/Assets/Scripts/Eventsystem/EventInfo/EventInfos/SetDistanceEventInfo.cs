using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDistanceEventInfo : BaseEventInfo
{
    public float MaxDistance { get; set; }

    public SetDistanceEventInfo(float distance)
    {
        MaxDistance = distance;
    }
}
