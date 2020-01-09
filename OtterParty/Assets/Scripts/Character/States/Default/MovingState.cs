using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player/MovingState")]
public class MovingState : CharacterBaseState
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float angle;
    [SerializeField]
    private LayerMask shoveTargetsMasks;
    [SerializeField]
    [Range(1f, 15f)]
    private float shoveCooldown;
    [SerializeField]
    [Range(1f, 15f)]
    private float jumpHeight;
    [SerializeField]
    private RuntimeAnimatorController defaultAnimations;
    public bool IsShoveOffCooldown { get; set; } = true;
    public override void Enter()
    {
        owner.PlayerState = PlayerController.CurrentPlayerState.MovingState;
        if (owner.Anim != null)
            owner.Anim.runtimeAnimatorController = defaultAnimations;
        Rigidbody playerBody = owner.GetComponent<Rigidbody>();
        playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        owner.OnMoveAction += Movement;
        owner.OnJumpAction += JumpAction;
        owner.OnShoveAction += ShoveAction;
        base.Enter();
    }

    private void ShoveAction()
    {
        if (IsShoveOffCooldown && owner.IsActive)
        {
            IsShoveOffCooldown = false;
            Cooldown.Instance.StartNewCooldown(shoveCooldown, this);
            var colliders = Manager.Instance.GetFrontConeHit(owner.transform.forward, owner.transform, shoveTargetsMasks, radius * ImportManager.Instance.Settings.ShoveRangeMultiplier, angle);
            owner.Anim.SetTrigger("Shove");
            if (colliders != null)
            {
                foreach (var item in colliders)
                {
                    if (item.CompareTag("Player") && item.gameObject != owner.gameObject)
                    {
                        var player = item.GetComponent<PlayerController>();
                        if(player.PlayerState != PlayerController.CurrentPlayerState.KnockBackState)
                        {                        
                            item.gameObject.transform.LookAt(new Vector3(owner.transform.position.x, item.gameObject.transform.position.y, owner.transform.position.z));
                            player.Transition<KnockbackState>();
                        }
                    }
                }
            }
        }
    }
    private void JumpAction()
    {
        owner.Jump(jumpHeight);
        owner.Transition<AirState>();
    }
    private void Movement(Vector2 inputDirection)
    {
        owner.InputDirection = inputDirection;
    }
    public override void Exit()
    {
        owner.OnShoveAction -= ShoveAction;
        owner.OnMoveAction -= Movement;   
        owner.OnJumpAction -= JumpAction;
    }
}
