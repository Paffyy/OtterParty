using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenArenaController : MonoBehaviour
{
    [SerializeField]
    private GameObject chickenBossPrefab;
    [SerializeField]
    private float timeBetweenRounds;
    [SerializeField]
    private float timeBetweenCharges;
    [SerializeField]
    [Range(1, 10)]
    private int numberOfChargePoints;
    [SerializeField]
    [Range(1, 10)]
    private int chargePointsMultiplier;
    private int chargePointIndex;
    private ChickenBoss chickenBoss;
    private Transform currentChargePoint;
    private List<Transform> allChargePoints = new List<Transform>();

    void Start()
    {
        chargePointIndex = 0;
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartGame);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopGame);
        foreach (Transform item in gameObject.transform)
        {
            allChargePoints.Add(item);
        }
        currentChargePoint = allChargePoints[0];
        StartGame(new StartMinigameEventInfo());
    }

    void Update()
    {
    }

    private void StartGame(BaseEventInfo e)
    {
        GameObject chicken = Instantiate(chickenBossPrefab);
        chickenBoss = chicken.GetComponent<ChickenBoss>();
        StartCoroutine("ChickenChargeGameLoop");
    }

    private List<Transform> RandomizeChargePoints()
    {
        List<Transform> newChargePoints = new List<Transform>();
        for (int i = 0; i < numberOfChargePoints; i++)
        {
            Transform newChargePoint = allChargePoints[Random.Range(0, allChargePoints.Count - 1)];
            while (newChargePoint == currentChargePoint)
            {
                newChargePoint = allChargePoints[Random.Range(0, allChargePoints.Count - 1)];
            }
            newChargePoints.Add(newChargePoint);
            currentChargePoint = newChargePoint;
        }
        return newChargePoints;
    }

    IEnumerator ChickenChargeGameLoop()
    {
        IncreaseDifficulty();
        yield return new WaitForSeconds(timeBetweenRounds);
        StartCoroutine("ChickenCharge");
    }

    IEnumerator ChickenCharge()
    {
        yield return new WaitForSeconds(timeBetweenCharges);
    }

    private void IncreaseDifficulty()
    {
        numberOfChargePoints += chargePointsMultiplier;
    }

    private void StopGame(BaseEventInfo e)
    {
        StopAllCoroutines();
    }
}
