using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player/MovingState")]
public class MovingState : CharacterBaseState
{
    public override void Enter()
    {
        Debug.Log("Enter movingstate");
        owner.OnMoveAction += Movement;
        owner.OnJumpAction += Jump;
        base.Enter();
    }
    private void Jump()
    {
        owner.Jump();
    }
    private void Movement(Vector2 inputDirection)
    {
        owner.InputDirection = inputDirection;
    }
    public override void Exit()
    {
        owner.OnMoveAction -= Movement;
        owner.InputDirection = Vector2.zero;
        owner.OnJumpAction -= Jump;
    }
}
