using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHat : MonoBehaviour
{
    private Sprite hatSprite;
    [SerializeField]
    private Sprite unavailableHatSprite;
    [SerializeField]
    private int hatIndex;
    [SerializeField]
    private Vector3 hatOffset;
    [SerializeField]
    private List<Material> hatMaterials;
    [SerializeField]
    private MeshRenderer meshRen;
    [SerializeField]
    private Vector3 hatRotation;
    [SerializeField]
    private Vector3 thumbnailScale;
    [SerializeField]
    private List<Sprite> hatSprites;

    public Vector3 ThumnailScale { get { return thumbnailScale; } }
    public List<Material> HatMaterials { get { return hatMaterials; } }
    public Vector3 HatRotation { get { return hatRotation; } }
    public Vector3 HatOffset { get { return hatOffset; } }
    public int HatIndex { get { return hatIndex; } }
    public bool IsAvailable { get; set; } = true;
    public Sprite CurrentSprite { get; set; }
    public Sprite HatSprite { get { return hatSprite; } }
    public Sprite UnavailableHatSprite { get { return unavailableHatSprite; } }

    public Sprite GetHatSprite(int index)
    {
        if (IsAvailable)
            return hatSprites[index];
        else
            return unavailableHatSprite;
    }

    public void SetHatMaterial(int index)
    {
        meshRen.material = hatMaterials[index];
    }

    public void SetAvailable(bool isAvailable)
    {
        if (isAvailable)
            CurrentSprite = hatSprite;
        else
            CurrentSprite = unavailableHatSprite;
    }
}
