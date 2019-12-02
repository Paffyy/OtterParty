using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenBoss : MonoBehaviour
{
    private Rigidbody chickenBody;
    [SerializeField]
    private float chargeSpeed;
    private int pointsIndex;
    private bool hasReachedChargePoint;
    private List<Transform> chargePoints = new List<Transform>();
    private float timeBetweenCharge;
    public bool IsCharging { get; set; }
    public bool IsReady { get; set; }

    void Start()
    {
        chickenBody = GetComponent<Rigidbody>();
       //spawn in animation
    }

    void FixedUpdate()
    {
        if (IsCharging)
        {

        }
    }

    public void SetNextChargePoints(List<Transform> newChargePoints)
    {
        chargePoints = newChargePoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // might change to limited amounts of lives instead of instant elimination
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, new EliminateEventInfo(other.gameObject));
        }
    }
}
