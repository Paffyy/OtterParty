using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StunObstacle : MonoBehaviour
{
    [SerializeField]
    private AudioClip knockBackSound;
    [SerializeField]
    private float volume;
    private Collider coll;
    private void Awake()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Transition<LockedKnockbackState>();
            if(knockBackSound != null)
            {
                EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(knockBackSound, volume));
            }
            Destroy(gameObject);
        }
    }
}
