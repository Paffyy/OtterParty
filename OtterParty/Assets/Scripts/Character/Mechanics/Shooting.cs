using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Test script (Has been migrated to state)", true)]
public class Shooting : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    [Range(1, 100.0f)]
    private int projectileRange;
    [SerializeField]
    [Range(0.25f, 2.0f)]
    private float cooldownDuration;

    public bool IsOffCooldown = true;
    private Rigidbody playerRigidbody;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void CheckCollision()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, projectileRange, targetMask);
        if (hit.collider != null)
        {
            if (EventHandler.Instance != null)
            {
                var e = new HitEventInfo(gameObject, hit.collider.gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, e);
            }
        }
        ApplySelfKnockback();
    }

    private void ApplySelfKnockback()
    {
        int knockbackMagnitude = Constants.Instance.DefaultKnockbackDistance;
        Vector3 knockbackVector = -transform.forward * knockbackMagnitude;
        playerRigidbody.velocity += knockbackVector;
    }

    void OnFire()
    {
        // Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
        IsOffCooldown = false;
        CheckCollision();
    }
}
