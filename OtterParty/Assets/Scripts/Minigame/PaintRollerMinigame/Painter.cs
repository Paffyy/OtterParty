using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Painter : MonoBehaviour
{
    [SerializeField] private MeshRenderer floor;
    [SerializeField] private Material[] playerMaterials;
    [SerializeField] private int brushSize = 1;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask paintingFloorMask;

    public MeshRenderer Floor { get { return floor; } set { floor = value; } }

    private int selectedMaterialIndex = 0;
    private RenderTexture splatMap;

    void Start()
    {
        selectedMaterialIndex = GameController.Instance.FindPlayerByGameObject(player).ID;
        floor = FindObjectOfType<CalculatePixelsScript>().gameObject.GetComponent<MeshRenderer>();
        splatMap = floor.material.GetTexture("_SplatMap") as RenderTexture;
        floor.material.SetTexture("_SplatMap", splatMap);
        foreach (var item in playerMaterials)
        {
            item.SetInt("_BrushSize", 1000 / brushSize);
        }
    }
    void Update()
    {
        if (player.GetComponent<PlayerController>().IsActive)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10, paintingFloorMask))
            {
                Paint(hit, 3);
            }
        }
    }
    private void Paint(RaycastHit hit, int intensity = 5)
    {
        for (int i = 0; i < intensity; i++)
        {
            playerMaterials[selectedMaterialIndex].SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y));
            RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, temp);
            Graphics.Blit(temp, splatMap, playerMaterials[selectedMaterialIndex]);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
