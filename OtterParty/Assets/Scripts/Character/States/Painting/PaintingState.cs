using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player/PaintingState")]
public class PaintingState : CharacterBaseState
{
    [SerializeField]
    private List<Material> paintingToolPlayerMaterials;


    public override void Enter()
    {
        
        owner.OnMoveAction += Movement;
        base.Enter();
    }

    private void Movement(Vector2 inputDirection)
    {
        owner.InputDirection = inputDirection;
    }
    public override void Exit()
    {
        owner.OnMoveAction -= Movement;
        owner.InputDirection = Vector2.zero;
    }
}