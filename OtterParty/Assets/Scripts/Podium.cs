using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    [SerializeField]
    private Transform endPos;
    [SerializeField]
    private GameObject podium;
    private Rigidbody body;
    private bool startElevatePodium;

    void Awake()
    {
        body = podium.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (startElevatePodium && endPos.position.y - podium.transform.position.y >= 0.2f)
        {
            body.MovePosition(podium.transform.position + new Vector3(transform.position.x, endPos.position.y, transform.position.z) * Time.deltaTime * 0.2f);
        }
        else
        {
            body.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    public void ElevatePodium()
    {
        startElevatePodium = true;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
