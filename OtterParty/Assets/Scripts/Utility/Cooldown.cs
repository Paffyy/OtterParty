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
    public void StartNewCooldown(int duration, bool cooldownReference, Shooting sender)
    {
        Task.Factory.StartNew(async () => {
            await Task.Delay(duration * 1000);
            sender.IsOffCooldown = true;
        });
                
    }
}
