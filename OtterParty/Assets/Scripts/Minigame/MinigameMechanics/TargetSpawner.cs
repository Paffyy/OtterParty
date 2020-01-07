using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private GameObject defaultTarget;
    [SerializeField]
    private GameObject bonusTarget;
    [SerializeField]
    private GameObject decoyTarget;
    private List<Transform> spawnLocations = new List<Transform>();
    [SerializeField]
    [Range(1, 5)]
    private int bonusTargetPoints;
    [SerializeField]
    [Range(-5, -1)]
    private int decoyTargetPoints;
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
        int randomTargetValue = Random.Range(0, 6);
        RandomizeTarget(randomTargetValue, t);
    }

    private void RandomizeTarget(int randomValue, Transform spawnLocation)
    {
        GameObject target;
        if (randomValue == 4)
        {
            target = Instantiate(bonusTarget, spawnLocation.position, bonusTarget.transform.rotation);
            target.GetComponent<MovingTarget>().SetValue(bonusTargetPoints);
        }
        else if (randomValue == 5)
        {
            target = Instantiate(decoyTarget, spawnLocation.position, decoyTarget.transform.rotation);
            target.GetComponentInChildren<MovingTarget>().SetValue(decoyTargetPoints);
        }
        else
        {
            target = Instantiate(defaultTarget, spawnLocation.position, defaultTarget.transform.rotation);
            target.GetComponentInChildren<MovingTarget>().SetValue(defaultPoints);
        }
        if(target != null)
        {
            Destroy(target, 10);
        }
    }
}
