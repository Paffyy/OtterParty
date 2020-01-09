using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceHandler : MonoBehaviour
{
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;
    private float maxDistance;
    private ShowDistanceMeterUI meterUI;
    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float sliderUpdateFrequence;
    private Dictionary<int, GameObject> playerMeters = new Dictionary<int, GameObject>();
    [SerializeField]
    private List<GameObject> meters = new List<GameObject>();

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.InstantiatedUIEvent, SetMeter);
        maxDistance = endPos.position.z - startPos.position.z;
        if (GameController.Instance != null)
        {
            foreach (var item in GameController.Instance.Players)
            {
                playerMeters.Add(item.ID, meters[item.ID]);
                meters[item.ID].SetActive(true);
            }
        }
    }

    private void SetMeter(BaseEventInfo e)
    {
        GameObjectEventInfo go = e as GameObjectEventInfo;
        if(go != null)
        {
            meterUI = go.gameObject.GetComponent<ShowDistanceMeterUI>();
            meterUI.UpdateMaxDistance(maxDistance);
            StartCoroutine("UpdateLoop");
        }
    }

    IEnumerator UpdateLoop()
    {
        yield return new WaitForSeconds(sliderUpdateFrequence);
        UpdateValues();
        StartCoroutine("UpdateLoop");
    }

    private void UpdateValues()
    {
        List<float> playerValues = new List<float>();
        if(GameController.Instance != null)
        {
            foreach (var item in GameController.Instance.Players)
            {
                var temp = maxDistance - (endPos.position.z - item.PlayerObject.transform.position.z);
                temp = Mathf.Clamp(temp, 0, maxDistance);
                var meter = playerMeters[item.ID];
                meter.transform.localScale = new Vector3(meter.transform.localScale.x, meter.transform.localScale.y, -temp / 10);
                playerValues.Add(temp);
            }
            meterUI.UpdateValues(playerValues);
        }
    }
}
