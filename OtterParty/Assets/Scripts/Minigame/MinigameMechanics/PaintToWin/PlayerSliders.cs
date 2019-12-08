using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSliders : MonoBehaviour
{
    [SerializeField]
    private Slider scoreSlider;
    [SerializeField]
    private TextMeshProUGUI percentageText;
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
        scoreSlider.maxValue = 100;
        scoreSlider.value = 0;
    }

    void Update()
    {
        if (startCounting)
        { 
            currentPercentage += Time.deltaTime * SpeedMultiplier;
            currentPercentage = Mathf.Clamp(currentPercentage, 0, Percentage);
            scoreSlider.value = currentPercentage;
            percentageText.text = currentPercentage.ToString("0.0") + "%";
        }
    }

}
