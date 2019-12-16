using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Painter : MonoBehaviour
{
    [SerializeField] private MeshRenderer floor;
    [SerializeField] private Material[] playerMaterials;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask paintingFloorMask;

    public MeshRenderer Floor { get { return floor; } set { floor = value; } }

    private int selectedMaterialIndex = 0;
    private RenderTexture splatMap;

    void Start()
    {
        if (GameController.Instance != null)
        {
            selectedMaterialIndex = GameController.Instance.FindPlayerByGameObject(player).ID;

        }
        else
        {
            selectedMaterialIndex = 0;
        }
        floor = FindObjectOfType<CalculatePixelsScript>().gameObject.GetComponent<MeshRenderer>();
        splatMap = floor.material.GetTexture("_SplatMap") as RenderTexture;
        floor.material.SetTexture("_SplatMap", splatMap);
    }
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Alpha1))
        //{
        //    selectedMaterialIndex = 0;
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha2))
        //{
        //    selectedMaterialIndex = 1;
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha3))
        //{
        //    selectedMaterialIndex = 2;
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha4))
        //{
        //    selectedMaterialIndex = 3;
        //}
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        //    {
        //        Paint(hit, 3);
        //    }
        //}
        //if (player.GetComponent<PlayerController>().IsActive || false)
        //{
        //    if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10, paintingFloorMask))
        //    {
        //        Paint(hit, 3);
        //    }
        //}
    }
    private void FixedUpdate()
    {
        if (player.GetComponent<PlayerController>().IsActive)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10, paintingFloorMask))
            {
                Paint(hit, 1);
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
