using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private bool isTestingUI;
    private void Start()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.TransitionEvent, Transition);
        }
        if (MinigameController.Instance != null && GameController.Instance != null || isTestingUI)
        {
            MinigameController.Instance.JoinPlayers();
            MinigameController.Instance.ToggleActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void Transition(BaseEventInfo e)
    {
        StartCoroutine("StartGameWithDelay");
    }
    IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(1f);
        StartGame();
    }
    public void StartGame()
    {
        MinigameController.Instance.StartMinigame();
        gameObject.SetActive(false);
    }
}
