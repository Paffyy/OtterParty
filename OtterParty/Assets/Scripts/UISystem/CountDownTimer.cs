using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    private float countDownTimer;
    [SerializeField]
    private TMP_Text countDownText;

    void Start()
    {
    }

    void Update()
    {
        countDownTimer -= 1 * Time.deltaTime;
        countDownText.text = countDownTimer.ToString("0");
        if(countDownTimer <= 3)
        {
            //start animation
        }
    }
}
