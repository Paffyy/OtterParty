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
    [Range(5f, 25f)]
    private int projectileRange;
    [SerializeField]
    [Range(0.5f, 2.0f)]
    private float cooldownDuration;
    [SerializeField]
    [Range(5f, 15f)]
    private float selfKnockbackValue;
    [SerializeField]
    private GameObject projectile;
    public bool IsOffCooldown { get; set; } = true;
    private ParticleSystem projectileParticle;

    public override void Enter()
    {
        projectileParticle = owner.GetComponent<ParticleSystem>();
        var main = projectileParticle.main;
        float lifetime = projectileRange / main.startSpeed.constant;
        main.startLifetime = lifetime;
        owner.OnMoveAction += Movement;
        owner.OnFireAction += Fire;
        base.Enter();
    }

    private void CheckCollision()
    {
        Physics.Raycast(owner.transform.position, owner.transform.forward, out RaycastHit hit, projectileRange, targetMask);
        if (hit.collider != null)
        {
            if (EventHandler.Instance != null && hit.collider.gameObject.CompareTag("Player"))
            {
                var e = new HitEventInfo(owner.gameObject, hit.collider.gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, e);
            }
            else if (EventHandler.Instance != null && hit.collider.gameObject.CompareTag("Enemy"))
            {
                var e = new HitEventInfo(owner.gameObject, hit.collider.gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, e);
            }
        }
        ApplySelfKnockback();
        //TODO Migrate to KnockbackState instead
    }

    private void ApplySelfKnockback()
    {
        Vector3 knockbackVector = -owner.transform.forward * selfKnockbackValue;
        owner.GetComponent<Rigidbody>().velocity += knockbackVector;
    }

    private void Fire()
    {
        if (owner.IsActive && IsOffCooldown)
        {
            FireProjectile();
            Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
            IsOffCooldown = false;
        }
    }

    private void FireProjectile()
    {
        if(owner.FirePoint != null)
        {
            var projectileClone = Instantiate(projectile, owner.FirePoint.position, owner.FirePoint.rotation);
            if (owner.PlayerGun != null)
            {
                owner.PlayerGun.GetComponent<GunAnimation>().TriggerKnockBack();
            }
            projectileClone.GetComponent<ProjectileMove>().PlayerThatShot = owner.gameObject;
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

