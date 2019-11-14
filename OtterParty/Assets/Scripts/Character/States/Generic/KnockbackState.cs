using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KnockbackState")]
public class KnockbackState : CharacterBaseState
{
    [SerializeField]
    [Range(0.5f,2.0f)]
    private float stunDuration;
    [SerializeField]
    [Range(1,10)]
    private float knockbackMagnitude;
    public bool IsKnockedBacked { get; set; }
    public override void Enter()
    {
        IsKnockedBacked = false;
        owner.InputDirection = Vector2.zero;
        owner.GetComponent<Rigidbody>().velocity = -owner.transform.forward * knockbackMagnitude;
        base.Enter();
        Cooldown.Instance.StartNewCooldown(stunDuration, this);
    }
    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (IsKnockedBacked)
        {
            owner.Transition<MovingState>();
        }

    }
}
