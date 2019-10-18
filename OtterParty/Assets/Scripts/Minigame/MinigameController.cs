using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    private GameModes gamemode;
    [SerializeField]
    private int mingameDuration;
    [SerializeField]
    [Range(1,10.0f)]
    private int endOfMatchDelay;
    [SerializeField]
    [Range(1, 10.0f)]
    private int tutorialDuration;
    [SerializeField]
    [Range(1, 10.0f)]
    private int startTimerDuration;

    private Dictionary<Player,bool> playersAlive;
    private int currentPoints = 1;
    private enum GameModes { FFA, AllvsOne, Team };

    private void Awake()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playersAlive.Add(item,true);
        }
    }
    private bool IsLastPlayerStanding()
    {
        int temp = 0;
        foreach (var item in playersAlive)
        {
            temp = item.Value ? temp : temp++;
        }
        return temp == GameController.Instance.Players.Count - 1;
    }
    public void EliminatePlayer(Player p)
    {
        playersAlive[p] = false;
        //PointSystem.Instance.AwardPlayer(p,currentPoints);
        if (IsLastPlayerStanding())
        {
            GameIsOver();
        }
        currentPoints++;
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
    public void GameIsOver()
    {
        StopAllCoroutines();
        FreezeAll();
        ShowStandings();
        StartCoroutine("GoBackToOverviewScene");
    }
    private IEnumerator GoToNexttScene()
    {
        yield return new WaitForSeconds(endOfMatchDelay);
        GameController.Instance.StartNextMinigame();
    }
    private void FreezeAll()
    {
        throw new NotImplementedException();
    }
    private void ShowStandings()
    {
        throw new NotImplementedException();
    }
}
