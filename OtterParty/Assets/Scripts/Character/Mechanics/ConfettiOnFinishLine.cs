using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiOnFinishLine : MonoBehaviour
{
    [SerializeField]
    private GameObject confetti;
    [SerializeField]
    private AudioClip startSound;
    [SerializeField]
    private float volume;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float confettiScale;
    private ParticleSystem ps;

    void Start()
    {
        ps = confetti.GetComponent<ParticleSystem>();
        EventHandler.Instance.Register(EventHandler.EventType.FinishLineEvent, InitiateConfetti);
    }

    private void InitiateConfetti(BaseEventInfo e)
    {
        if(startSound != null)
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(startSound, volume, 1));
        }
        ps.transform.localScale = new Vector3(confettiScale, confettiScale, confettiScale);
        ps.Play();
    }
}
