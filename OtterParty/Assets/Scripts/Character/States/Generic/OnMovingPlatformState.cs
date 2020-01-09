using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/OnMovingPlatformState")]
public class OnMovingPlatformState : CharacterBaseState
{
    [SerializeField]
    [Range(1f, 10f)]
    private float jumpHeight;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float angle;
    [SerializeField]
    private LayerMask shoveTargetsMasks;
    [SerializeField]
    [Range(1f, 15f)]
    private float shoveCooldown;
    public bool IsShoveOffCooldown { get; set; } = true;

    public override void Enter()
    {
        owner.PlayerState = PlayerController.CurrentPlayerState.OnMovingPlatformState;
        owner.IsOnMovingPlatform = true;
        owner.OnMoveAction += Movement;
        owner.OnJumpAction += JumpAction;
        owner.OnShoveAction += ShoveAction;
        base.Enter();
    }

    private void Movement(Vector2 inputDirection)
    {
        owner.InputDirection = inputDirection;
    }
    
    private void JumpAction()
    {
        owner.Jump(jumpHeight);
        owner.Transition<AirState>();
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
                    if(item.CompareTag("Player") && item.gameObject != owner.gameObject)
                    {
                        var player = item.GetComponent<PlayerController>();
                        if (player.PlayerState != PlayerController.CurrentPlayerState.KnockBackState)
                        {
                            item.gameObject.transform.LookAt(new Vector3(owner.transform.position.x, item.gameObject.transform.position.y, owner.transform.position.z));
                            player.Transition<KnockbackState>();
                        }
                    }
                }
            }
        }
    }

    public override void Exit()
    {
        owner.IsOnMovingPlatform = false;
        owner.OnShoveAction -= ShoveAction;
        owner.OnMoveAction -= Movement;
        owner.OnJumpAction -= JumpAction;
    }
}
