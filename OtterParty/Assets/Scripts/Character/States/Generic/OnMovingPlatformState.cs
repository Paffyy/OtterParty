using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/OnMovingPlatformState")]
public class OnMovingPlatformState : CharacterBaseState
{
    [SerializeField]
    [Range(1f, 10f)]
    private float jumpHeight;

    public override void Enter()
    {
        owner.IsOnMovingPlatform = true;
        owner.OnMoveAction += Movement;
        owner.OnJumpAction += JumpAction;
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

    public override void Exit()
    {
        owner.IsOnMovingPlatform = false;
        owner.OnMoveAction -= Movement;
        owner.OnJumpAction -= JumpAction;
    }
}
