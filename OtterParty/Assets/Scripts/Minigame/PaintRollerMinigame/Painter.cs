using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Painter : MonoBehaviour
{
    [SerializeField] private MeshRenderer floor;
    [SerializeField] private Material[] playerMaterials;
    [SerializeField] private int brushSize = 1;

    private int selectedMaterialIndex = 0;
    private RenderTexture splatMap;

    void Start()
    {
        splatMap = floor.material.GetTexture("_SplatMap") as RenderTexture;
        floor.material.SetTexture("_SplatMap", splatMap);
        foreach (var item in playerMaterials)
        {
            item.SetInt("_BrushSize", 1000 / brushSize);
        }
      
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedMaterialIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedMaterialIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedMaterialIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedMaterialIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedMaterialIndex = 4;
        }
        //if (Input.GetKey(KeyCode.Mouse0))
        //{ 
        //    var ray = new Ray(transform.position, Vector3.down);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        Paint(hit);
        //    }
        //}
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
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
