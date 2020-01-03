using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCameraScript : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f,0.1f)]
    private float speed;
    void FixedUpdate()
    {
        var movement = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement -= Vector3.up;
        }
        transform.position = Vector3.Lerp(transform.position, transform.position += movement, speed);
    }
}
