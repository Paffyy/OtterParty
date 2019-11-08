﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinataBehaviour : MonoBehaviour
{
    [SerializeField]
    private float standStillDuration;
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private float walkDistance;
    private NavMeshAgent navMeshAgent;
    private bool isQuitting;
    private Animator anim;
    private MeshRenderer mesh;
    public int Points { get; set; }
    public Action OnDestroyed { get; internal set; }

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
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
        navMeshAgent.velocity = rndDirection * walkDistance;
        StartCoroutine("BehaviourLoop");
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void SetValue(Material mat, int pts)
    {
        mesh.material = mat;
        Points = pts;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            Instantiate(particles, transform.position, transform.rotation);
            OnDestroyed?.Invoke();
        }
    }
}
