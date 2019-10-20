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
    public override void Enter()
    {
        owner.GetComponent<Rigidbody>().velocity = -owner.transform.forward * Constants.Instance.DefaultKnockbackDistance;
        base.Enter();
        Task.Factory.StartNew(async() => 
        {
            await Task.Delay((int)(stunDuration * 1000));
            owner.Transition<MovingState>();
        });
    }
}
