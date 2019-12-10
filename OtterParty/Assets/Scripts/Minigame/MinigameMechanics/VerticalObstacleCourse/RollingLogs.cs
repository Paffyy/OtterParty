using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingLogs : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private GameObject logPrefab;
    [SerializeField]
    [Range(1f, 50f)]
    private float logSpeed;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float spawnInterval;

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartMechanics);
       // StartMechanics(new StartMinigameEventInfo()); //Debug testing
    }

    private void StartMechanics(BaseEventInfo e)
    {
        StartCoroutine("SpawnLog");
    }

    IEnumerator SpawnLog()
    {
        InstantiateLog();
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine("SpawnLog");
    }

    private void InstantiateLog()
    {
        GameObject log = Instantiate(logPrefab, spawnPosition.position, logPrefab.transform.rotation);
        log.GetComponent<Log>().ActivateLog(logSpeed);
    }
}
