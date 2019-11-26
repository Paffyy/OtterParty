using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/LockedAirState")]
public class LockedAirState : CharacterBaseState
{
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private float skinWidth;

    public override void Enter()
    {
        owner.PlayerState = PlayerController.CurrentPlayerState.LockedAirState;
        owner.IsInLockedMovement = true;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.IsGrounded())
        {
            owner.Transition<LockedMovementState>();
        }
    }

    public override void Exit()
    {
        owner.IsInLockedMovement = false;
    }
}
