using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    [Range(1, 100.0f)]
    private int projectileRange;
    [SerializeField]
    [Range(1, 5f)]
    private int cooldownDuration;

    public bool IsOffCooldown = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsOffCooldown)
        {
            Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
            IsOffCooldown = false;
            CheckCollision();
        }
        Debug.DrawRay(transform.position, transform.forward);
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
    }
}
