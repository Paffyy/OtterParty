using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/CharacterBaseState")]
public class CharacterBaseState : State
{
    protected PlayerController owner;

    public override void Initialize(StateMachine owner)
    {
        this.owner = (PlayerController)owner;
    }
}
