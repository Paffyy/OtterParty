using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEventInfo : BaseEventInfo
{
    public GameObject gameObject { get; set; }

    public GameObjectEventInfo(GameObject obj)
    {
        gameObject = obj;
    }
}
