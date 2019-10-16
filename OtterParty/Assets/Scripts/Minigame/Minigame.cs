using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    private int id;
    [SerializeField]
    private string minigameName;
    [SerializeField]
    private GameModes gamemode;
    [SerializeField]
    private int mingameDuration;
    [SerializeField]
    private GameObject tutorialUI;

    private bool winCondition;

    private enum GameModes { FFA, AllvsOne, Team  };

    public void CheckWinCondition()
    {
        bool temp;
        //insert Wincondition here
        temp = true;
        if (temp)
        {
            GameIsOver();
        }
    }
    public void ShowTutorialUI()
    {

    }
    public void StartMinigameTimer(int duration = 60)
    {
        StartCoroutine("MinigameTimer", duration);
    }
    IEnumerator MinigameTimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        GameIsOver();
    }
    private void GameIsOver()
    {
        StopAllCoroutines();
        //Display stuff here and procceed to next
    }
}
