using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHat : MonoBehaviour
{
    [SerializeField]
    private Sprite hatSprite;
    [SerializeField]
    private Sprite unavailableHatSprite;
    [SerializeField]
    private int hatIndex;

    public int HatIndex { get { return hatIndex; } }
    public bool IsAvailable { get; set; } = true;
    public Sprite HatSprite { get { return hatSprite; } }
    public Sprite UnavailableHatSprite { get { return unavailableHatSprite; } }
}
