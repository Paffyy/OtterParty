using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private GameObject target;
    private List<Transform> spawnLocations = new List<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform item in transform)
        {
            spawnLocations.Add(item);
        }
    }
    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartLoop);
    }   
    private void StartLoop(BaseEventInfo e)
    {
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
        var obj = Instantiate(target,t.position,t.rotation);
        Destroy(obj, 10);
    }
}
