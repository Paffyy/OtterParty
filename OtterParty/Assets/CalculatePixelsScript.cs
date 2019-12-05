using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePixelsScript : MonoBehaviour
{
    // Start is called before the first frame update
    private RenderTexture splatMapTexture;
    private Texture2D tex2d;
    private float[] colors = new float[4];
    private int pixelCount;
    void Start()
    {
        tex2d = new Texture2D(2024, 2024);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetPixels();
            Color[] pixels = tex2d.GetPixels();
            pixelCount = pixels.Length;
            foreach (var item in pixels)
            {
                AddToColors(item);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            var temp = "";
            foreach (var item in colors)
            {
                temp += item / pixelCount + " - ";
            }
            Debug.Log(temp);
        }
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
    }
}
