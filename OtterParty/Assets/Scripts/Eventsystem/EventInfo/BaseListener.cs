using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseListener : MonoBehaviour
{
    public abstract void Register();
    void Start()
    {
        Register();
    }
}
