using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenBoss : MonoBehaviour
{
    private int pointsIndex;
    private List<Transform> chargePoints;
    private NavMeshAgent agent;
    private System.Action onWaitUntilNext;
    private float waitTimeBetweenCharges;
    private bool shouldRotate;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    [Range(1, 10)]
    private float chargeSpeedMultiplier;
    private float defaultChargeSpeed;
    private Animator anim;
    private bool hasNewTarget;
    [SerializeField]
    private AudioClip chickenHitWall;
    [SerializeField]
    private float chickenHitWallVolume;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultChargeSpeed = agent.speed;
        anim = GetComponent<Animator>();
        //spawn in animation
    }

    void Update()
    {
        if (agent.velocity.magnitude > maxSpeed)
        {
            agent.velocity = agent.velocity.normalized * maxSpeed;
        }
        if (!IsCharging())
        {
            if (shouldRotate)
            {
                RotateTowardsTarget();
            }
            if (hasNewTarget)
            {
                hasNewTarget = false;
                agent.velocity = Vector3.zero;
                anim.SetBool("IsRunning", false);
                onWaitUntilNext?.Invoke();
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (chargePoints[pointsIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6f);
    }

    private void SetDestination()
    {
        agent.SetDestination(chargePoints[pointsIndex].position);
        agent.isStopped = false;
        anim.SetBool("IsRunning", true);
        onWaitUntilNext += NextCharge;
        hasNewTarget = true;
    }

    private bool IsCharging()
    {
        return agent.remainingDistance != Mathf.Infinity && agent.remainingDistance > 0.2f;
    }

    private void NextCharge()
    {
        onWaitUntilNext -= NextCharge;
        SoundEventInfo sei = new SoundEventInfo(chickenHitWall, chickenHitWallVolume, 2);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        StartCoroutine("WaitUntilNextCharge");
    }

    IEnumerator WaitUntilNextCharge()
    {
        CheckNextChargeTarget();
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTimeBetweenCharges);
        if (pointsIndex != 0)
        {
            SetDestination();
        }
    }

    private void CheckNextChargeTarget()
    {
        if (pointsIndex < chargePoints.Count - 1)
        {
            agent.speed *= chargeSpeedMultiplier;
            pointsIndex++;
            shouldRotate = true;
        }
        else
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.StartNextRoundEvent, new StartMinigameEventInfo());
            pointsIndex = 0;
        }
    }

    public void SetNextChargePoints(List<Transform> newChargePoints, float timeBetweenCharges)
    {
        agent.speed = defaultChargeSpeed;
        waitTimeBetweenCharges = timeBetweenCharges;
        chargePoints = newChargePoints;
        SetDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.IsVulnerable)
            {
                other.gameObject.transform.LookAt(new Vector3(transform.position.x, other.gameObject.transform.position.y, transform.position.z));
                player.Transition<ImprovedKnockBackState>();
                EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, new HitEventInfo(null, other.gameObject));
            }
        }
    }
}
