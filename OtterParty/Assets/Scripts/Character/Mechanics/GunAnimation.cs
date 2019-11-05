using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerKnockBack()
    {
        anim.SetTrigger("Shot");
    }

    public void TriggerReload()
    {
        anim.SetTrigger("Reload");
    }
}
