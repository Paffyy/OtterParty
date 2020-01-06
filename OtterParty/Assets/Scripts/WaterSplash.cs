using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField]
    private AudioClip waterSplash;
    [SerializeField]
    private float waterSplashVolume;
    [SerializeField]
    private GameObject waterSplashEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TransformEventInfo tei = new TransformEventInfo(other.gameObject.transform.position, waterSplashEffect.transform.rotation, waterSplashEffect);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ParticleEvent, tei);
            SoundEventInfo sei = new SoundEventInfo(waterSplash, waterSplashVolume, 1);
            EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        }
    }
}
