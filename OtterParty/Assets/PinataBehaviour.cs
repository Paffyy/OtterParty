using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinataBehaviour : MonoBehaviour
{
    [SerializeField]
    private float standStillDuration;
    [SerializeField]
    private float walkDistance;
    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        StartCoroutine("BehaviourLoop");
    }

    private IEnumerator BehaviourLoop()
    {
        yield return new WaitForSeconds(standStillDuration);
        Vector3 rndDirection = Manager.Instance.GetRandomDirectionVector();
        navMeshAgent.velocity += rndDirection * walkDistance;
        StartCoroutine("BehaviourLoop");
    }
}
