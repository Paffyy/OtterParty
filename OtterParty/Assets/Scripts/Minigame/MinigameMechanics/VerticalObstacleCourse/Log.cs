using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private float speed;
    private bool logActive;
    private Rigidbody logBody;

    void Start()
    {
        logBody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (logActive)
        {
            logBody.velocity = new Vector3(1, -0.2f, 0) * speed;
        }
    }

    public void ActivateLog(float logSpeed)
    {
        speed = logSpeed;
        logActive = true;
        Destroy(gameObject, 15);
    }
}
