﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : BaseListener
{
    [SerializeField]
    private AudioSource primaryAudioSource;
    [SerializeField]
    private AudioSource secondaryAudioSource;
    [SerializeField]
    [Range(0, 5f)]
    private float primaryAudioSourceVolume;
    [SerializeField]
    [Range(0, 5f)]
    private float secondaryAudioSourceVolume;
    [SerializeField] private GameObject source = null;

    public override void Register()
    {
        primaryAudioSource.volume = primaryAudioSourceVolume;
        EventHandler.Instance.Register(EventHandler.EventType.SoundEvent, PlaySound);
    }

    private void PlaySound(BaseEventInfo e)
    {
        SoundEventInfo eventInfo = e as SoundEventInfo;
        if (eventInfo != null)
        { 
            primaryAudioSource.PlayOneShot(eventInfo.SoundClip);
            //if (!primaryAudioSource.isPlaying)
            //{
            //    UsePrimaryAudioSource(eventInfo);
            //}
            //else
            //{
            //    UseSecondaryAudioSource(eventInfo);
            //}
        }
    }

    //private void PlaySound(SoundEventInfo e)
    //{
    //    SoundEventInfo sei = e as SoundEventInfo;
    //    GameObject instance = Instantiate(source);
    //    AudioSource audio = instance.GetComponent<AudioSource>();
    //    audio.PlayOneShot(sei.SoundClip);
    //    Destroy(instance, sei.SoundClip.length);
    //}

    private void UsePrimaryAudioSource(SoundEventInfo eventInfo)
    {
        if (eventInfo.Volume > 0)
        {
            primaryAudioSource.volume = eventInfo.Volume;
        }
        else
        {
            primaryAudioSource.volume = primaryAudioSourceVolume;
        }
        if(eventInfo.SoundClip != null)
        {
            primaryAudioSource.clip = eventInfo.SoundClip;
            primaryAudioSource.Play();
        }
    }

    private void UseSecondaryAudioSource(SoundEventInfo eventInfo)
    {
        if (eventInfo.Volume > 0)
        {
            secondaryAudioSource.volume = eventInfo.Volume;
        }
        else
        {
            secondaryAudioSource.volume = secondaryAudioSourceVolume;
        }
        if (eventInfo.SoundClip != null)
        {
            secondaryAudioSource.clip = eventInfo.SoundClip;
            secondaryAudioSource.Play();
        }
    }
}
