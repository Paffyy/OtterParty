using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColors
{
    #region Singleton
    private PlayerColors() { }
    private static PlayerColors instance;
    public static PlayerColors Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerColors();
            return instance;
        }
    }
    #endregion

    public List<Color> Colors = new List<Color>()
    {
        new Color(216,27,96,1), // magenta
        new Color(30,136,229,1), // lt blue
        new Color(255,193,7,1), // yellow
        new Color(0,255,105,1), // Green
    };
}
