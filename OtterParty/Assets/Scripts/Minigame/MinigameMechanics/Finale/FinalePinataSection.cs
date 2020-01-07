using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalePinataSection : MonoBehaviour
{
    [SerializeField]
    private GameObject pinataPrefab;
    [SerializeField]
    private List<GameObject> playerGates;
    [SerializeField]
    private AudioClip defaultPinataHitAudio;
    [SerializeField]
    private float defaultPinataHitAudioVolume;
    [SerializeField]
    private List<GameObject> arrowSigns;
    private List<Transform> pinataSpawnPositions = new List<Transform>();
    private Dictionary<GameObject, List<GameObject>> playerPinatas = new Dictionary<GameObject, List<GameObject>>();


    void Awake()
    {
        foreach (Transform item in gameObject.transform)
        {
            pinataSpawnPositions.Add(item);
        }
    }

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, EliminatePinata);
    }

    private void EliminatePinata(BaseEventInfo e)
    {
        HitEventInfo hitEventInfo = e as HitEventInfo;
        if(hitEventInfo != null)
        {
            GameObject playerThatShot = hitEventInfo.ObjectThatFired;
            GameObject objectHit = hitEventInfo.ObjectHit;
            if (playerPinatas[playerThatShot].Contains(objectHit))
            {
                HandlePinataHitEvent(objectHit, playerThatShot);
                objectHit.SetActive(false);
                if (playerPinatas[playerThatShot].Count > 1)
                {
                    playerPinatas[playerThatShot].Remove(objectHit);
                }
                else
                {
                    OpenGate(playerThatShot);
                }
            }
        }
    }
    private void OpenGate(GameObject playerThatShot)
    {
        Player p = GameController.Instance.FindPlayerByGameObject(playerThatShot);
        playerGates[p.ID].GetComponent<Animator>().SetTrigger("OpenGate");
        arrowSigns[p.ID].SetActive(true);
        playerGates[p.ID].GetComponent<BoxCollider>().enabled = false;
    }
    public void InitPinataSection(PointSystem pointSystem)
    {
        foreach (var item in pointSystem.GetCurrentScore())
        {
            List<GameObject> pinatas = new List<GameObject>();
            playerPinatas.Add(item.Key.PlayerObject, pinatas);
            for (int i = 0; i < item.Value; i++)
            {
                playerPinatas[item.Key.PlayerObject].Add(SpawnPinata(item.Key.ID));
            }
        }
    }
    private void HandlePinataHitEvent(GameObject hitObject, GameObject playerThatShot)
    {
        Vector3 position = hitObject.transform.position;
        Quaternion rotation = hitObject.transform.rotation;
        GameObject deathParticles = hitObject.GetComponent<PinataBehaviour>().ParticleObjects[GameController.Instance.FindPlayerByGameObject(playerThatShot).ID];
        TransformEventInfo tei = new TransformEventInfo(position, rotation, deathParticles);
        EventHandler.Instance.FireEvent(EventHandler.EventType.ParticleEvent, tei);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(hitObject.GetComponent<PinataBehaviour>().HitAudio, hitObject.GetComponent<PinataBehaviour>().HitAudioVolume, 1));
    }

    private GameObject SpawnPinata(int index)
    {
        GameObject pinata = Instantiate(pinataPrefab, pinataSpawnPositions[index].position , pinataSpawnPositions[index].rotation);
        pinata.GetComponent<PinataBehaviour>().HitAudio = defaultPinataHitAudio;
        pinata.GetComponent<PinataBehaviour>().HitAudioVolume = defaultPinataHitAudioVolume;
        return pinata;
    }
}
