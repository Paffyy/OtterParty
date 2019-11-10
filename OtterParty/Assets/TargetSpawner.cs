﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private GameObject target;
    private List<Transform> spawnLocations = new List<Transform>();
    [SerializeField]
    private Sprite specialChicken;
    [SerializeField]
    [Range(1, 5)]
    private int specialPoints;
    [SerializeField]
    [Range(-5, -1)]
    private int rottenChickenPoints;
    [SerializeField]
    private Sprite rottenChickenSprite;
    [SerializeField]
    [Range(1, 5)]
    private int defaultPoints;
    private int playerCount;

    void Awake()
    {
        foreach (Transform item in transform)
        {
            spawnLocations.Add(item);
        }
    }
    void Start()
    {
        if (GameController.Instance != null)
        {
            playerCount = GameController.Instance.Players.Count;
        }
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartLoop);
    }   
    private void StartLoop(BaseEventInfo e)
    {
        spawnInterval *= 1 - (1 - (1 / (float)playerCount));
        StartCoroutine("SpawnLoop");
    }
    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnTarget();
        StartCoroutine("SpawnLoop");
    }
    private void SpawnTarget()
    {
        int index = Manager.Instance.GetRandomInt(0, spawnLocations.Count);
        Transform t = spawnLocations[index];
        int randomChickenValue = Random.Range(0, 6);
        var obj = Instantiate(target,t.position,t.rotation);
        AssignChickenValue(randomChickenValue, obj);
        Destroy(obj, 10);
    }

    private void AssignChickenValue(int randomValue, GameObject chickenObj)
    {
        var chicken = chickenObj.GetComponentInChildren<MovingTarget>();
        if (randomValue == 4)
        {
            chicken.SetValue(specialChicken, specialPoints);
        }
        else if (randomValue == 5)
        {
            chicken.SetValue(rottenChickenSprite, rottenChickenPoints);
        }
        else
        {
            chicken.Points = defaultPoints;
        }
    }
}
