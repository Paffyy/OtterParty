using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AirState")]
public class AirState : CharacterBaseState
{

    public override void Enter()
    {
        owner.OnMoveAction += Movement;
        owner.IsGrounded = false;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.IsGrounded)
        {
            owner.Transition<MovingState>();
        }
    }

    private void Movement(Vector2 inputDirection)
    {
        owner.InputDirection = inputDirection;
    }

    public override void Exit()
    {
        owner.InputDirection = Vector2.zero;
        owner.OnMoveAction -= Movement;
    }

}
