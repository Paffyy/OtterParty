using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUpUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> readyUpImages = new List<Image>();
    [SerializeField]
    private List<Image> backgrounds = new List<Image>();
    private Dictionary<Player, bool> playerReady = new Dictionary<Player, bool>();
    private void Start()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.ReadyUpEvent, ReadyUp);
        }
    }

    public void PlayerJoined(Player p)
    {
        playerReady.Add(p, false);
        backgrounds[p.ID].gameObject.SetActive(true);
    }
    private void ReadyUp(BaseEventInfo e)
    {
        ReadyUpEventInfo readyUpInfo = e as ReadyUpEventInfo;
        if (readyUpInfo != null)
        {
            Player p = GameController.Instance.FindPlayerByID(readyUpInfo.PlayerID);
            if (playerReady.ContainsKey(p))
            {
                playerReady[p] = !playerReady[p];
                readyUpImages[p.ID].gameObject.SetActive(playerReady[p]);
  
                if (IsAllPlayersReady() && playerReady.Count > 1)
                {
                    TransitionEventInfo tei = new TransitionEventInfo();
                    EventHandler.Instance.FireEvent(EventHandler.EventType.TransitionEvent, tei);
                    EventHandler.Instance.Unregister(EventHandler.EventType.ReadyUpEvent, ReadyUp);
                }
            }
        }
    }
    private bool IsAllPlayersReady()
    {
        int temp = 0;
        foreach (var item in playerReady)
        {
            temp = item.Value ? temp + 1  : temp;
        }
        return temp == playerReady.Count;
    }

}
