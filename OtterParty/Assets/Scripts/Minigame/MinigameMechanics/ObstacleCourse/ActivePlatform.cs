using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent = gameObject.transform;
    }

    protected void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
    }
}
