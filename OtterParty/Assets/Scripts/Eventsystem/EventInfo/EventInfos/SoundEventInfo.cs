using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventInfo : BaseEventInfo
{
    public AudioClip SoundClip { get; set; }
    public float Volume { get; set; }

    public SoundEventInfo(AudioClip soundClip, float volume)
    {
        Volume = volume;   
        SoundClip = soundClip;
    }
}
