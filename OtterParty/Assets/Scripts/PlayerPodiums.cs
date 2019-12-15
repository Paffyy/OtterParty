using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPodiums : MonoBehaviour
{
    private float currentPercentage;
    private bool startCounting;
    public float Percentage { get; set; }
    public float SpeedMultiplier { get; set; }

    public void StartCountingPoints()
    {
        startCounting = true;
    }

    void Start()
    {
        currentPercentage = 0;
    }

    void Update()
    {
        if (startCounting)
        {
            currentPercentage += Time.deltaTime * SpeedMultiplier;
            currentPercentage = Mathf.Clamp(currentPercentage, 0, Percentage);
        }
    }

}
