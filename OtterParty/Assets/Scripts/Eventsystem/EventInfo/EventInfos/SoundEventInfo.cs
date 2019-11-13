using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventInfo : BaseEventInfo
{
    public AudioClip SoundClip { get; set; }

    public SoundEventInfo(AudioClip soundClip)
    {
        SoundClip = soundClip;
    }
}
