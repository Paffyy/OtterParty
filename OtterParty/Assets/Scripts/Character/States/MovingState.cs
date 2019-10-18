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
        base.Enter();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    //private void OnJump()
    //{
    //    Debug.Log("Jumping");
    //    owner.Jump();
    //}
}
