using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject spikes;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float spikesActiveTime;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float spikesResetTime;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float startDelay;



    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartMechanics);
      //  StartMechanics(new StartMinigameEventInfo()); //Debug testing
    }

    private void StartMechanics(BaseEventInfo e)
    {
        StartCoroutine("ActivateSpikes");
    }

    IEnumerator ActivateSpikes()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine("ToggleSpikes");

    }

    IEnumerator ToggleSpikes()
    {
        spikes.SetActive(true);
        yield return new WaitForSeconds(spikesActiveTime);
        spikes.SetActive(false);
        yield return new WaitForSeconds(spikesResetTime);
        StartCoroutine("ToggleSpikes");
    }

}
