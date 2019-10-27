using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private float tutorialDuration; // Todo replace with aiming controller
    [SerializeField]
    private TextMeshProUGUI tutorialDurationText;
    void Start()
    {
        if (MinigameController.Instance != null && GameController.Instance != null)
        {
            StartCoroutine("StartMinigame");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartMinigame()
    {
        yield return new WaitForSeconds(tutorialDuration);
        MinigameController.Instance.StartMinigameTimer();
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (tutorialDurationText != null)
        {
            tutorialDuration -= Time.deltaTime;
            tutorialDurationText.text = tutorialDuration.ToString("0");
        }
    }
}
