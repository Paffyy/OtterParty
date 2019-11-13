using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : BaseListener
{
    [SerializeField]
    private AudioSource primaryAudioSource;
    [SerializeField]
    [Range(0, 5f)]
    private float primaryAudioSourceVolume;

    public override void Register()
    {
        primaryAudioSource.volume = primaryAudioSourceVolume;
        EventHandler.Instance.Register(EventHandler.EventType.SoundEvent, PlaySound);
    }

    private void PlaySound(BaseEventInfo e)
    {
        SoundEventInfo eventInfo = e as SoundEventInfo;
        if(eventInfo != null)
        {
            primaryAudioSource.clip = eventInfo.SoundClip;
            primaryAudioSource.Play();
        }
    }
}
