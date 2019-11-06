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

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.InstantiatedUIEvent, SetMeter);
        maxDistance = endPos.position.z - startPos.position.z;
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
        foreach (var item in GameController.Instance.Players)
        {
            var temp = maxDistance - (endPos.position.z - item.PlayerObject.transform.position.z);
            temp = Mathf.Clamp(temp, 0, maxDistance);
            playerValues.Add(temp);
        }
        meterUI.UpdateValues(playerValues);
    }
}
