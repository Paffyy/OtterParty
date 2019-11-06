using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDistanceMeterUI : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField]
    private List<Slider> playerImages;
    [SerializeField]
    private List<Image> playerHearts;
    private Dictionary<int, List<GameObject>> hearts = new Dictionary<int, List<GameObject>>();

    private void Awake()
    {
        foreach (var item in playerImages)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (GameController.Instance != null && MinigameController.Instance != null)
        {
            InstantiateUI();
            EventHandler.Instance.FireEvent(EventHandler.EventType.InstantiatedUIEvent, new GameObjectEventInfo(gameObject));
        }
    }

    public void UpdateValues(List<float> playerValues)
    {
        var players = GameController.Instance.Players;
        for (int i = 0; i < players.Count; i++)
        {
            playerImages[i].GetComponent<Slider>().value = playerValues[i];
        }
    }

    private void InstantiateUI()
    {
        var players = GameController.Instance.Players;
        for (int i = 0; i < players.Count; i++)
        {
            playerImages[i].gameObject.SetActive(true);
        }
    }

    public void UpdateMaxDistance(float distance)
    {
        var players = GameController.Instance.Players;
        for (int i = 0; i < players.Count; i++)
        {
            playerImages[i].GetComponent<Slider>().maxValue = distance;
        }
    }
}
