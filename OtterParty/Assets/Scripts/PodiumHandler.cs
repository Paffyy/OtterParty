using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> podiumPrefabs;
    [SerializeField]
    private List<Transform> podiumPlacements;
    [SerializeField]
    private float showPlacementsDelay;

    void Awake()
    {
        foreach (Transform item in gameObject.transform)
        {
            podiumPlacements.Add(item);
        }
    }

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, InitiateCeremony);
    }

    private void InitiateCeremony(BaseEventInfo e)
    {
        StartCoroutine("ShowPlacements");
    }

    IEnumerator ShowPlacements()
    {
        yield return new WaitForSeconds(showPlacementsDelay);
        AssignPodiums();
    }

    private void AssignPodiums()
    {
        foreach (var player in GameController.Instance.Players)
        {
            int i = 0;
            var podium = Instantiate(podiumPrefabs[i]);
        }
    }
}
