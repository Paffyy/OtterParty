using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player/MovingState")]
public class MovingState : CharacterBaseState
{
    [SerializeField]
    [Range(1f, 15f)]
    private float jumpHeight;

    public override void Enter()
    {
        Rigidbody playerBody = owner.GetComponent<Rigidbody>();
        playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        owner.OnMoveAction += Movement;
        owner.OnJumpAction += JumpAction;
        base.Enter();
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
        owner.OnMoveAction -= Movement;   
        owner.OnJumpAction -= JumpAction;
    }
}
