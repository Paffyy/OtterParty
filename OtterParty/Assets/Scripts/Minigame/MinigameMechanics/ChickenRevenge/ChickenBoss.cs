using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBoss : MonoBehaviour
{
    private Rigidbody chickenBody;
    void Start()
    {
        chickenBody = GetComponent<Rigidbody>();
       //spawn in animation
    }

    void Update()
    {
        
    }

    public void ChargeToTarget(List<Vector3> chargePositions, float timeBetweenCharges)
    {
        foreach (Vector3 position in chargePositions)
        {
           // chickenBody.MovePosition()
        }
    }
}
