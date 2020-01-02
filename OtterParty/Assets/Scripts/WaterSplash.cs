using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField]
    private AudioClip waterSplash;
    [SerializeField]
    private float waterSplashVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundEventInfo sei = new SoundEventInfo(waterSplash, waterSplashVolume, 1);
            EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        }
    }
}
