using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Range(1, 5f)]
    private int countDownTimer;

    private PointSystem minigamePointSystem;
    private Dictionary<Player,bool> playersAlive;
    private int currentPoints = 1;
    private enum GameModes { FFA, AllvsOne, Team, Points };

    private void Awake()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playersAlive.Add(item,true);
        }
    }

    public void EliminatePlayer(Player p) // FFA
    {
        playersAlive[p] = false;
        var playerPoints = new Dictionary<Player, int>();
        playerPoints.Add(p, currentPoints);
        minigamePointSystem.UpdateScore(playerPoints);
        currentPoints++;
        if (IsLastPlayerStanding())
        {
            minigamePointSystem.UpdateScore(playerPoints);
            GameIsOver(); 
        }
    }
    private bool IsLastPlayerStanding() // FFA
    {
        int temp = 0;
        foreach (var item in playersAlive)
        {
            temp = item.Value ? temp : temp++;
        }
        return temp == GameController.Instance.Players.Count - 1;
    }
    public void StartMinigame()
    {
        StartCoroutine("StartCountDown");
    }
    IEnumerator StartCountDown() // TODO Display The CountDown UI
    {
        yield return new WaitForSeconds(countDownTimer);
        StartMinigameTimer();
        EnablePlayers();
    }

    private void EnablePlayers()
    {
        throw new NotImplementedException();
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
        //Adds point to the last guy and adds points to all remaining players if timer runs out
        var playerPoints = new Dictionary<Player, int>();
        foreach (var item in playersAlive.Where(x => x.Value)) 
        {
            playerPoints.Add(item.Key, currentPoints);
        }
        minigamePointSystem.UpdateScore(playerPoints);

        StopAllCoroutines();
        FreezeAll();
        ShowStandingsUI();
        StartCoroutine("GoToNextScene");
    }
    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(endOfMatchDelay);
        GameController.Instance.StartNextMinigame();
    }
    private void FreezeAll()
    {
        throw new NotImplementedException();
    }
    private void ShowStandingsUI()
    {
        throw new NotImplementedException();
    }

    /* Tutorial UI -> Display Untill every1 clicks
     * CountDownTimer UI -> Show Timer when counting down
     * Show updates on points/eliminations during game
     * Show Scores adding to main score and prep next game
     */
}
