using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataSpawner : MonoBehaviour
{
    private List<GameObject> alivePinatas = new List<GameObject>();
    private List<Transform> pinataSpawnPoints = new List<Transform>();
    [SerializeField]
    private GameObject pinataPrefab;
    [SerializeField]
    private float respawnInterval;
    [SerializeField]
    private int numberOfPinatasAliveAtOnce;
    [SerializeField]
    private Material specialPinataMaterial;
    [SerializeField]
    [Range(1, 5)]
    private int pinataValue;
    [SerializeField]
    [Range(1, 5)]
    private int specialPinataValue;

    private void Awake()
    {
        foreach (Transform item in gameObject.transform)
        {
            pinataSpawnPoints.Add(item);
        }
    }
    private void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartLoop);
    }
    public void StartLoop(BaseEventInfo e)
    {
        StartCoroutine("SpawnPinataLoop");
    }
    private IEnumerator SpawnPinataLoop()
    {
        yield return new WaitForSeconds(respawnInterval);
        if (alivePinatas.Count < numberOfPinatasAliveAtOnce)
        {
            SpawnPinata();
        }
        StartCoroutine("SpawnPinataLoop");
    }

    private void SpawnPinata()
    {
        int randomSpawnPointIndex = UnityEngine.Random.Range(0, pinataSpawnPoints.Count-1 );
        pinataPrefab.transform.position = pinataSpawnPoints[randomSpawnPointIndex].position;
        int randomPinataValue = UnityEngine.Random.Range(0, 5);
        var pinata = Instantiate(pinataPrefab);
        AssignPinataValue(randomPinataValue, pinata);
        pinata.GetComponent<PinataBehaviour>().OnDestroyed += () => 
        {
            if (alivePinatas.Contains(pinata))
            {
                alivePinatas.Remove(pinata);
            }
        };
        alivePinatas.Add(pinata);
    }

    private void AssignPinataValue(int randomValue, GameObject pinataObj)
    {
        var pinataObject = pinataObj.GetComponent<PinataBehaviour>();
        if(randomValue == 4)
        {
            pinataObject.SetValue(specialPinataMaterial, specialPinataValue);
        } 
        else
        {
            pinataObject.Points = pinataValue;
        }
    }
}
