using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolMiniGameController : MonoBehaviour
{
    private Dictionary<Symbol, List<GameObject>> platforms;

    enum Symbol
    {
        Cyan,
        Orange,
        Red
    };

    [SerializeField]
    private Symbol symbol;
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private GameObject symbolPlatform;
}
