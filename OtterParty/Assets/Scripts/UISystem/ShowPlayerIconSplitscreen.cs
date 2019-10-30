using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerIconSplitscreen : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField]
    private List<Image> playerImages;
    private void Awake()
    {
        foreach (var item in playerImages)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        StartPointsUI();
    }
    public void StartPointsUI()
    {
        if (GameController.Instance != null && MinigameController.Instance != null)
        {
            var players = GameController.Instance.Players;
            for (int i = 0; i < players.Count; i++)
            {
                playerImages[i].gameObject.SetActive(true);
            }
        }
    }
}

