using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes
{
    public int OverviewScene { get; set; }
    #region Singleton
    private Scenes() { }
    private static Scenes instance;
    public static Scenes Instance
    {
        get
        {
            if (instance == null)
                instance = new Scenes();
            return instance;
        }
    }
    #endregion
}
