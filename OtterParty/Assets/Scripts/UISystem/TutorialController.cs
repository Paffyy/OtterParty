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
    [SerializeField]
    private bool isTestingUI;

    private void Start()
    {
        tutorialDurationText.text = tutorialDuration.ToString();
        if (MinigameController.Instance != null && GameController.Instance != null || isTestingUI)
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
        if (MinigameController.Instance != null && GameController.Instance != null)
        {
            MinigameController.Instance.JoinPlayers();
            MinigameController.Instance.StartMinigame();
        }
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
