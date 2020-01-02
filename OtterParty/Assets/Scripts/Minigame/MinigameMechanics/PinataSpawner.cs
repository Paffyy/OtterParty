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
    private float respawnInterval = 1;
    [SerializeField]
    private int numberOfPinatasAliveAtOnce;
    [SerializeField]
    private Material specialPinataMaterial;
    [SerializeField]
    private Material decoyPinataMaterial;
    [SerializeField]
    [Range(1, 5)]
    private int pinataValue;
    [SerializeField]
    [Range(1, 5)]
    private int specialPinataValue;
    [SerializeField]
    [Range(-1, -5)]
    private int decoyPinataValue;
    [SerializeField]
    private AudioClip defaultPinataHitAudio;
    [SerializeField]
    private float defaultPinataHitAudioVolume;
    [SerializeField]
    private AudioClip specialPinataHitAudio;
    [SerializeField]
    private float specialPinataHitAudioVolume;
    [SerializeField]
    private AudioClip decoyPinataHitAudio;
    [SerializeField]
    private float decoyPinataHitAudioVolume;
    [SerializeField]
    private bool isDebugging;

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
        if (isDebugging)
        {
            StartCoroutine("SpawnPinataLoop");
        }
    }
    public void StartLoop(BaseEventInfo e)
    {
        respawnInterval *= 1 - (1 - (1 / (float)GameController.Instance.Players.Count));
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
            pinataObject.SetValue(specialPinataMaterial, specialPinataValue, specialPinataHitAudio, specialPinataHitAudioVolume);
        }
        else if (randomValue == 3)
        {
            pinataObject.SetValue(decoyPinataMaterial, decoyPinataValue, decoyPinataHitAudio, decoyPinataHitAudioVolume);
            Destroy(pinataObject.gameObject, 5);
        }
        else
        {
            pinataObject.Points = pinataValue;
            pinataObject.HitAudio = defaultPinataHitAudio;
            pinataObject.HitAudioVolume = defaultPinataHitAudioVolume;
        }
    }
}
