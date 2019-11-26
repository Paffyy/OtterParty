using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHat : MonoBehaviour
{
    [SerializeField]
    private Sprite unavailableHatSprite;
    [SerializeField]
    private Vector3 hatOffset;
    [SerializeField]
    private List<Material> hatMaterials;
    [SerializeField]
    private MeshRenderer meshRen;
    [SerializeField]
    private Vector3 hatRotation;
    [SerializeField]
    private List<Sprite> hatSprites;
    public int SelectedByPlayerIndex { get; set; }

    public Vector3 HatRotation { get { return hatRotation; } }
    public Vector3 HatOffset { get { return hatOffset; } }
    public bool IsAvailable { get; set; } = true;

    public Sprite GetHatSprite(int index)
    {
        if (IsAvailable || SelectedByPlayerIndex == index)  
            return hatSprites[index];
        else
            return unavailableHatSprite;
    }
    public void SetHatMaterial(int index)
    {
        meshRen.material = hatMaterials[index];
    }
}
