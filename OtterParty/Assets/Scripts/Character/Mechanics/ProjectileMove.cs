using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] [Range(1f, 40f)] private float speed;
    [SerializeField] private GameObject muzzlePrefab;
    [SerializeField] private GameObject hitPrefab;
    public GameObject PlayerThatShot { get; set; }

    void Start()
    {
        if(muzzlePrefab != null)
        {
            var muzzleClone = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleClone.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleClone.GetComponent<ParticleSystem>();
            if(psMuzzle != null)
            {
                Destroy(muzzleClone, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleClone.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleClone, psChild.main.duration);
            }
        }
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        ContactPoint contact = collision.contacts[0];
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            var e = new HitEventInfo(PlayerThatShot, collision.gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, e);
        }
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        if(hitPrefab != null)
        {
            var hitClone = Instantiate(hitPrefab, position, rotation);
            var psHit = hitClone.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitClone, psHit.main.duration);
            }
            else
            {
                var psChild = hitClone.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitClone, psChild.main.duration);
            }
        }
        Destroy(gameObject);
    }
}
