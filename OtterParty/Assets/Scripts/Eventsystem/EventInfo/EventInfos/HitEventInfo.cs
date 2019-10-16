using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEventInfo : BaseEventInfo
{
    public GameObject ObjectThatFired;
    public GameObject ObjectHit;
    public HitEventInfo(GameObject objectThatFired, GameObject objectHit)
    {
        ObjectThatFired = objectThatFired;
        ObjectHit = objectHit;
    }
}
