using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEventInfo : BaseEventInfo
{
    public Vector3 objectTransformPosition { get; set; }
    public Quaternion objectTransformRotation { get; set; }
    public GameObject objectParticleSystem { get; set; }
   
    public TransformEventInfo(Vector3 pos, Quaternion rot, GameObject objParticleSystem)
    {
        objectTransformPosition = pos;
        objectTransformRotation = rot;
        objectParticleSystem = objParticleSystem;
    } 
}
