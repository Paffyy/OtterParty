using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleHitEventInfo : BaseEventInfo
{
    public GameObject hitObject { get; set; }

    public FinaleHitEventInfo(GameObject hit)
    {
        hitObject = hit;
    }
}
