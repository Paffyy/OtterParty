using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolPlatformHandler : MonoBehaviour
{
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

    void Start()
    {
        SetColor();
    }

    private void SetColor()
    {
        switch (symbol)
        {
            case Symbol.Cyan:
                symbolPlatform.GetComponent<MeshRenderer>().material = materials[0];
                break;
            case Symbol.Orange:
                symbolPlatform.GetComponent<MeshRenderer>().material = materials[1];
                break;
            case Symbol.Red:
                symbolPlatform.GetComponent<MeshRenderer>().material = materials[2];
                break;
            default:
                break;
        }
    }

    public void FallDown()
    {

    }

    public void Respawn()
    {

    }
}
