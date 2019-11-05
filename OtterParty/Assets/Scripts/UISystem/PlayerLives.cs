using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public Player player { get; set; }
    public Image PlayerHeart
    {
        get { return playerHeart; }
        set { playerHeart = value; }
    }

    [SerializeField]
    private Image playerHeart;

    void Start()
    {
    }



}
