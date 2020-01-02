using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonSelectedSound;

    public void PlaySelectionSound()
    {
        SoundEventInfo sei = new SoundEventInfo(buttonSelectedSound, 0, 1);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
    }
}
