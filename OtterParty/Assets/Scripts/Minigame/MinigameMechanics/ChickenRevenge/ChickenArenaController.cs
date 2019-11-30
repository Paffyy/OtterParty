using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenArenaController : MonoBehaviour
{
    [SerializeField]
    private GameObject chickenBoss;
    [SerializeField]
    private List<Transform> chargePoints;
    [SerializeField]

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartGame);
    }

    private void StartGame(BaseEventInfo e)
    {
       GameObject chicken = Instantiate(chickenBoss);
       StartCoroutine("ChickenChargeGameLoop");
    }

    private void RandomizeChargePoints()
    {

    }

    //IEnumerator ChickenChargeGameLoop()
    //{

    //}
}
