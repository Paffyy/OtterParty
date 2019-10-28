using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    [Range(2.0f, 10.0f)]
    private float rotationSpeed;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, -1, 0), rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Killed by camera");
            // todo
        }
    }
}
