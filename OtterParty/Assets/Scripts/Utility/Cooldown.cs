using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Cooldown
{
    private Cooldown() { }
    private static Cooldown instance;
    public static Cooldown Instance
    {
        get
        {
            if (instance == null)
                instance = new Cooldown();
            return instance;
        }
    }
    public void StartNewCooldown(float duration, ShootingState sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));
            sender.IsOffCooldown = true;
        });
    }
    public void StartNewCooldown(float duration, PlayerAim sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));
            sender.IsOffCooldown = true;
        });
    }
    public void StartNewCooldown(float duration, KnockbackState sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));
            sender.IsKnockedBacked = true;
        });
    }

    public void StartNewCooldown(float duration, ImprovedKnockBackState sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));
            sender.IsKnockedBacked = true;
        });
    }

    public void StartNewCooldown(float duration, PlayerController sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));

            sender.IsVulnerable = true;
        });
    }

    public void StartNewCooldown(float duration, MovingState sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));
            sender.IsShoveOffCooldown = true;
        });
    }
    public void StartNewCooldown(float duration, OnMovingPlatformState sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay((int)(duration * 1000));
            sender.IsShoveOffCooldown = true;
        });
    }
}
