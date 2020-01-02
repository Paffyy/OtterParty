using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript_Logs : MonoBehaviour
{

    private AudioSource source;
    private float volLowRange = 0.05f;
    private float volHighRange = 0.2f;
    private float pitchLowRange = 0.90f;
    private float pitchHighRange = 1.10f;

    public AudioClip clip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("SoundTrigger"))
        {

            source.pitch = Random.Range(pitchLowRange, pitchHighRange);
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(clip, vol);
        }
    }
}