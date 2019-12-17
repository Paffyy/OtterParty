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
    private List<Transform> pinataSpawnPositions = new List<Transform>();
    private Dictionary<GameObject, int> playerPinatas = new Dictionary<GameObject, int>();

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
            HandlePinataHitEvent(hitEventInfo.ObjectHit, playerThatShot);
            hitEventInfo.ObjectHit.SetActive(false);
            if(playerPinatas[playerThatShot] > 1)
            {
                playerPinatas[playerThatShot]--;
            }
            else
            {
                OpenGate(playerThatShot);
            }
        }
    }
    private void OpenGate(GameObject playerThatShot)
    {
        Player p = GameController.Instance.FindPlayerByGameObject(playerThatShot);
        playerGates[p.ID].SetActive(false);
    }
    public void InitPinataSection(PointSystem pointSystem)
    {
        foreach (var item in pointSystem.GetCurrentScore())
        {
            playerPinatas.Add(item.Key.PlayerObject, item.Value);
            for (int i = 0; i < item.Value; i++)
            {
                SpawnPinata(item.Key.ID);
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
    }

    private void SpawnPinata(int index)
    {
        var pinata = Instantiate(pinataPrefab, pinataSpawnPositions[index].position, pinataSpawnPositions[index].rotation);
    }
}
