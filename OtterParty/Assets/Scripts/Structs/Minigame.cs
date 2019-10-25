using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;
    public int SceneIndex
    {
        get { return sceneIndex; }
        set { sceneIndex = value; }
    }

    public string Name { get; set; }
    private void Awake()
    {
        Name = gameObject.name;
    }
}
