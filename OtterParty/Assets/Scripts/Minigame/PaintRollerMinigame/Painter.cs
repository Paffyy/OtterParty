using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Painter : MonoBehaviour
{
    private RenderTexture splatMap;
    [SerializeField] private MeshRenderer floor;
    [SerializeField] private Material[] playerMaterials;
    private int selectedMaterialIndex = 0;


    void Start()
    {
        splatMap = new RenderTexture(2024, 2024, 0, RenderTextureFormat.ARGBFloat);
        RenderTexture.active = splatMap;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;
        floor.material.SetTexture("_SplatMap", splatMap);
      
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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                playerMaterials[selectedMaterialIndex].SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y));
                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatMap, temp);
                Graphics.Blit(temp, splatMap, playerMaterials[selectedMaterialIndex]);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
