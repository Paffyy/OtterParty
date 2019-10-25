﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player 
{
    public Player()
    {
    }
    public Player(int id,string name, InputDevice device)
    {
        ID = id;
        Name = name;
        Device = device;
    }
    public int ID { get; set; }
    public string Name { get; set; }
    public InputDevice Device { get; set; }
    public int HatIndex { get; set; }
}
