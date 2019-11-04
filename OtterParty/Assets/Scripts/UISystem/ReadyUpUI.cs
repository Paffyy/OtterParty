using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyUpUI : MonoBehaviour
{
    private Dictionary<Player, bool> playerReady = new Dictionary<Player, bool>();
    private void Start()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.StartReadyUpSequence, InitializeReadyUpUI);
        }

    }
    private void InitializeReadyUpUI(BaseEventInfo e)
    {
        foreach (var item in GameController.Instance.Players)
        {
            playerReady.Add(item, false);
        }
        gameObject.SetActive(true);
        EventHandler.Instance.Register(EventHandler.EventType.ReadyUpEvent, ReadyUp);
    }

    private void ReadyUp(BaseEventInfo e)
    {
        ReadyUpEventInfo readyUpInfo = e as ReadyUpEventInfo;
        Player p = GameController.Instance.FindPlayerByGameObject(readyUpInfo.PlayerObject);
        if (playerReady.ContainsKey(p))
        {
            playerReady[p] = !playerReady[p];
            // Enable UI Toggle
            if (IsAllPlayersReady())
            {
                //Transition to game / next
                TransitionEventInfo tei = new TransitionEventInfo();
                EventHandler.Instance.FireEvent(EventHandler.EventType.TransitionEvent, tei);
            }
        }
    }
    private bool IsAllPlayersReady()
    {
        int temp = 0;
        foreach (var item in playerReady)
        {
            temp = item.Value ? temp : temp + 1;
        }
        return temp == playerReady.Count;
    }

}
