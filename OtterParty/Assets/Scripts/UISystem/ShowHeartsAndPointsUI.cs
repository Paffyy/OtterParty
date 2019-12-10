using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHeartsAndPointsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject heartsUI;
    [SerializeField]
    private GameObject pointsUI;

    void Start()
    {
        InitUI();
    }
    private void InitUI()
    {
        heartsUI.SetActive(true);
        pointsUI.SetActive(true);     
    }
}
