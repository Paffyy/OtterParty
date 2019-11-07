using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolPlatformHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject symbolPlatform;
    [SerializeField]
    private Material[] materials;
    
    enum platformID
    {
        one,
        two,
        three
    };

    [SerializeField]
    private platformID ID;

    public int PlatformID { get { return (int)ID; } }
    public bool IsPlaced { get; set; }

    void Start()
    {
        SetColor();
    }

    private void SetColor()
    {
        switch (ID)
        {
            case platformID.one:
                symbolPlatform.GetComponent<MeshRenderer>().material = materials[0];
                break;
            case platformID.two:
                symbolPlatform.GetComponent<MeshRenderer>().material = materials[1];
                break;
            case platformID.three:
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
