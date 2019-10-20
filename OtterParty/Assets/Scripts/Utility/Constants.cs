using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants 
{
    public int DefaultKnockbackDistance { get; set; } = 8;

    #region Singleton
    private Constants() { }
    private static Constants instance;
    public static Constants Instance
    {
        get
        {
            if (instance == null)
                instance = new Constants();
            return instance;
        }
    }
    #endregion

}

