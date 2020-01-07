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
    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float pickUpSpawnInterval;
    [SerializeField]
    private GameObject defaultPickUp;
    [SerializeField]
    private GameObject spawnArea;
    private ChickenBoss chickenBoss;
    private Transform currentChargePoint;
    private List<Transform> allChargePoints = new List<Transform>();


    void Awake()
    {
        foreach (Transform item in gameObject.transform)
        {
            allChargePoints.Add(item);
        }
        currentChargePoint = allChargePoints[0];
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartGame);
    }
    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartNextRoundEvent, StartNextRound);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopGame);
    }

    private void StartNextRound(BaseEventInfo e)
    {
        StartCoroutine("ChickenChargeGameLoop");
    }

    private void StartGame(BaseEventInfo e)
    {
        GameObject chicken = Instantiate(chickenBossPrefab, allChargePoints[0].position, Quaternion.identity);
        chickenBoss = chicken.GetComponent<ChickenBoss>();
        chicken.transform.position = allChargePoints[0].position;
        StartCoroutine("ChickenChargeGameLoop");
        StartCoroutine("SpawnPickUps");
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
        yield return new WaitForSeconds(timeBetweenRounds);
        chickenBoss.SetNextChargePoints(RandomizeChargePoints(), timeBetweenCharges);
        IncreaseDifficulty();
    }
    IEnumerator SpawnPickUps()
    {
        SpawnPickUp();
        yield return new WaitForSeconds(pickUpSpawnInterval);
        StartCoroutine("SpawnPickUps");
    }

    private void SpawnPickUp()
    {
        GameObject pickUp = Instantiate(defaultPickUp, GenerateRandomSpawnPosition(defaultPickUp), defaultPickUp.transform.rotation);
    }

    private Vector3 GenerateRandomSpawnPosition(GameObject pickUp)
    {
        MeshRenderer mesh = spawnArea.GetComponent<MeshRenderer>();
        float xBounds = mesh.bounds.size.x / 2;
        float zBounds = mesh.bounds.size.z / 2;
        Vector3 center = mesh.bounds.center;
        MeshRenderer pickUpMesh = pickUp.GetComponent<MeshRenderer>();
        float pickUpOffset = (pickUpMesh.bounds.size.y / -2) + pickUpMesh.GetComponent<MeshRenderer>().bounds.center.y;
        Vector3 spawnPos = new Vector3(center.x + Random.Range(-xBounds, xBounds), spawnArea.transform.position.y + pickUpOffset, center.z + Random.Range(-zBounds, zBounds));
        return spawnPos;
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
