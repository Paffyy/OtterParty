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
        if (IsCharging)
        {
            //Debug.Log(chargePoints[pointsIndex].position);
            //chickenBody.MovePosition(transform.position + chargePoints[pointsIndex].position * chargeSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, chargePoints[pointsIndex].position, chargeSpeed * Time.deltaTime);
        }
        if(IsCharging && Vector3.Distance(transform.position, chargePoints[pointsIndex].position) < 0.2f)
        {
            Debug.Log("in distance");
            if (currentWaitTime <= 0)
            {
                if(pointsIndex < chargePoints.Count)
                {
                    pointsIndex++;
                }
                else
                {
                    EventHandler.Instance.FireEvent(EventHandler.EventType.StartNextRoundEvent, new StartMinigameEventInfo());
                    pointsIndex = 0;
                }
                currentWaitTime = waitTime;
            }
            else
            {
                currentWaitTime -= Time.deltaTime;
            }
        }
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
