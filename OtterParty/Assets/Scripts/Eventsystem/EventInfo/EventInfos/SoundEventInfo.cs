using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventInfo : BaseEventInfo
{
    public AudioClip SoundClip { get; set; }
    public float Volume { get; set; }
    public int AudioSourceID { get; set; }

    public SoundEventInfo(AudioClip soundClip, float volume, int audioSourceID)
    {
        if (audioSourceID < 1 || audioSourceID > 2)
            AudioSourceID = 1;
        else
            AudioSourceID = audioSourceID;
        Volume = volume;   
        SoundClip = soundClip;
    }
}
