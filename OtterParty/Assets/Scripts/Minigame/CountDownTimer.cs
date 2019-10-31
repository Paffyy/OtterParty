using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    private float timer;
    private bool miniGameHasStarted;
    [SerializeField]
    [Range(0, 30)]
    private int warningTimerThreshold;
    private TMP_Text textField;

    void Start()
    {
        textField = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (miniGameHasStarted)
        {
            if (timer < warningTimerThreshold + 1)
            {
                textField.color = new Color32(247, 63, 35, 255);
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                textField.text = timer.ToString("0");
            }
        }
    }

    public void InitiateTimer(int duration)
    {
        miniGameHasStarted = true;
        timer = duration;
    }

}
