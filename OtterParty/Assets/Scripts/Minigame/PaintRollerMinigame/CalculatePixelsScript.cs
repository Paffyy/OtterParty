using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePixelsScript : MonoBehaviour
{
    private RenderTexture splatMapTexture;
    private Texture2D tex2d;
    private float[] colors = new float[4];
    private int pixelCount;
    private RenderTexture splatMap;
    void Awake()
    {
        splatMap = new RenderTexture(2024, 2024, 0, RenderTextureFormat.ARGBFloat);
        RenderTexture.active = splatMap;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;
        GetComponent<MeshRenderer>().material.SetTexture("_SplatMap", splatMap);
        tex2d = new Texture2D(2024, 2024);
    }
    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.G))
    //    {
    //        GetPlayerPercentage();
    //    }
    //}
    public List<float> GetPlayerPercentage()
    {
        List<float> playerPercentages = new List<float>();
        SetPixels();
        Color[] pixels = tex2d.GetPixels();
        pixelCount = pixels.Length;
        colors = new float[4];
        foreach (var item in pixels)
        {
            AddToColors(item);
        }
        foreach (var item in colors)
        {
            float a = (float)(item / pixelCount) * 100;
            Debug.Log(a);
            playerPercentages.Add(a);
        }
        return playerPercentages;
    }

    private void AddToColors(Color item)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            switch (i)
            {
                case 0:
                    colors[i] += item.r;
                    break;
                case 1:
                    colors[i] += item.g;
                    break;
                case 2:
                    colors[i] += item.b;
                    break;
                case 3:
                    colors[i] += 1 - item.a;
                    break;
                default:
                    break;
            }
        }
    }

    private void SetPixels()
    {
        splatMapTexture = GetComponent<MeshRenderer>().material.GetTexture("_SplatMap") as RenderTexture;
        RenderTexture.active = splatMapTexture;
        tex2d.ReadPixels(new Rect(0, 0, splatMapTexture.width, splatMapTexture.height), 0, 0);
        tex2d.Apply();
        RenderTexture.active = null;
    }

}
