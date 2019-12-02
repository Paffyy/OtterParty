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
    private float waitTime;
    private float currentWaitTime;
    public bool IsCharging { get; set; }
    public bool IsReady { get; set; }

    void Start()
    {
        chickenBody = GetComponent<Rigidbody>();
   
       //spawn in animation
    }

    void FixedUpdate()
    {
        Charge();
        CheckDistanceToTarget();
    }

    private void Charge()
    {
        if (IsCharging)
        {
            //chickenBody.MovePosition(transform.position + chargePoints[pointsIndex].position * chargeSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, chargePoints[pointsIndex].position, chargeSpeed * Time.deltaTime);
        }
    }

    private void CheckDistanceToTarget()
    {
        if (IsCharging && Vector3.Distance(transform.position, chargePoints[pointsIndex].position) < 0.2f)
        {
            CheckTimeToNextCharge();
        }
    }

    private void CheckTimeToNextCharge()
    {
        if (currentWaitTime <= 0)
        {
            CheckNextChargeTarget();
        }
        else
        {
            currentWaitTime -= Time.deltaTime;
        }
    }

    private void CheckNextChargeTarget()
    {
        if (pointsIndex < chargePoints.Count)
        {
            pointsIndex++;
        }
        else
        {
            IsCharging = false;
            EventHandler.Instance.FireEvent(EventHandler.EventType.StartNextRoundEvent, new StartMinigameEventInfo());
            pointsIndex = 0;
        }
        currentWaitTime = waitTime;
    }

    public void SetNextChargePoints(List<Transform> newChargePoints, float timeBetweenCharges)
    {
        waitTime = timeBetweenCharges;
        chargePoints = newChargePoints;
        IsCharging = true;
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
