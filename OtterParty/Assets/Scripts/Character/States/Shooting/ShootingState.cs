using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player/ShootingState")]
public class ShootingState : CharacterBaseState
{
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    [Range(1, 100.0f)]
    private int projectileRange;
    [SerializeField]
    [Range(0.25f, 2.0f)]
    private float cooldownDuration;
    [SerializeField]
    [Range(5f, 10f)]
    private float selfKnockbackValue;
    public bool IsOffCooldown { get; set; } = true;
    public override void Enter()
    {
        owner.OnMoveAction += Movement;
        owner.OnFireAction += Fire;
        base.Enter();
    }

    private void CheckCollision()
    {
        Physics.Raycast(owner.transform.position, owner.transform.forward, out RaycastHit hit, projectileRange, targetMask);
        if (hit.collider != null)
        {
            if (EventHandler.Instance != null)
            {
                var e = new HitEventInfo(owner.gameObject, hit.collider.gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, e);
            }
        }
        Debug.Log("fire");
        ApplySelfKnockback();
    }

    private void ApplySelfKnockback()
    {
        Vector3 knockbackVector = -owner.transform.forward * selfKnockbackValue;
        owner.GetComponent<Rigidbody>().velocity += knockbackVector;
    }

    private void Fire()
    {
        if (IsOffCooldown)
        {
            Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
            IsOffCooldown = false;
            CheckCollision();
        }
    }
    private void Movement(Vector2 inputDirection)
    {
        owner.InputDirection = inputDirection;
    }
    public override void Exit()
    {
        owner.OnMoveAction -= Movement;
        owner.InputDirection = Vector2.zero;
        owner.OnFireAction -= Fire;
        IsOffCooldown = true;
    }
}

