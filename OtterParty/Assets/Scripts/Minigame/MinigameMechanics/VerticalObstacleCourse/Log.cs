using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private float speed;
    private bool logActive;
    private Rigidbody logBody;
    [SerializeField]
    private Vector3 logDirection;

    void Start()
    {
        logBody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (logActive)
        {
            logBody.velocity = logDirection * speed;
        }
    }

    public void ActivateLog(float logSpeed)
    {
        speed = logSpeed;
        logActive = true;
        Destroy(gameObject, 15);
    }
}
