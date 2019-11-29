using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiMachine : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private AudioClip startSound;
    [SerializeField]
    private float volume;

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, InitiateConfetti);
    }

    private void InitiateConfetti(BaseEventInfo e)
    {
        if(startSound != null)
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(startSound, volume));
        }
        ps.Play();
    }
}
