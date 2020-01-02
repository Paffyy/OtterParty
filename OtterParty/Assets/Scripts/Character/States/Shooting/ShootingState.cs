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
    private GameObject[] projectiles;
    private GameObject projectilePrefab;
    public bool IsOffCooldown { get; set; } = true;
    private ParticleSystem projectileParticle;
    [SerializeField]
    private RuntimeAnimatorController playerGunAnimations;
    [SerializeField]
    private AudioClip shotgunSoundEffect;

    public override void Enter()
    {
        if(owner.Anim != null)
              owner.Anim.runtimeAnimatorController = playerGunAnimations;
        owner.PlayerGun.SetActive(true);
        owner.PlayerState = PlayerController.CurrentPlayerState.ShootingState;
        projectileParticle = owner.GetComponent<ParticleSystem>();
        var main = projectileParticle.main;
        if (GameController.Instance != null)
        {
            projectilePrefab = projectiles[GameController.Instance.FindPlayerByGameObject(owner.gameObject).ID];
        }
        else
        {
            projectilePrefab = projectiles[0];
        }
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
            if (EventHandler.Instance != null)
            {
                EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(shotgunSoundEffect, 0.5f, 1));
            }
            FireProjectile();
            
            Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
            IsOffCooldown = false;
        }
    }

    private void FireProjectile()
    {
        if(owner.FirePoint != null)
        {
            var projectileClone = Instantiate(projectilePrefab, owner.FirePoint.position, owner.FirePoint.rotation);
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

