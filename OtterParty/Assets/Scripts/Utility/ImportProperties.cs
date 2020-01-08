using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

public class ImportProperties : MonoBehaviour
{
    #region Singleton Implementation
    private ImportProperties() { }
    private static ImportProperties instance;
    public static ImportProperties Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ImportProperties>();
            return instance;
        }
    }
    #endregion
    public ImportedSettings Settings { get; set; }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadImportedProperties();
    }
    private async void LoadImportedProperties()
    {
        string fileName = "settings.txt";
        try
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string text = await sr.ReadToEndAsync();
                Settings = JsonUtility.FromJson<ImportedSettings>(text);
            }
            if (!Settings.ValidateSuccess())
            {
                await CreateFile(fileName);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            await CreateFile(fileName);
        }
    }

    private async Task CreateFile(string fileName)
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            Settings = new ImportedSettings();
            var output = JsonUtility.ToJson(Settings, true);
            await sw.WriteLineAsync(output);
        }
    }
    
}

[Serializable]
public class ImportedSettings
{
    public bool ValidateSuccess()
    {
        foreach(PropertyInfo prop in typeof(ImportedSettings).GetProperties())
        {
            var floatValue = prop.GetValue(this, null) as float?;
            if (floatValue != null && floatValue == default(float) && floatValue <= 0)
            {
                Debug.Log("caught default or < 0" + prop.Name);
                return false;
            }
        }
        return true;
    }
    [SerializeField] private float finalScoreMultiplier = 2; //TODO
    public float FinaleScoreMultiplier
    {
        get { return finalScoreMultiplier;  }
        set { finalScoreMultiplier = value; }
    }

    [SerializeField] private float playerSpeed = 6; //TODO
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }

    [SerializeField] private float jumpHeight = 6; //TODO
    public float JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }

    [SerializeField] private float shoveForce = 6; // TODO
    public float ShowForce
    {
        get { return shoveForce; }
        set { shoveForce = value; }
    }

    [SerializeField] private float shoveRange = 6; // TODO
    public float ShoveRange
    {
        get { return shoveRange; }
        set { shoveRange = value; }
    }

    [SerializeField] private float runAndJumpModifier = 6; // TODO
    public float RunAndJumpModifier
    {
        get { return runAndJumpModifier; }
        set { runAndJumpModifier = value; }
    }
}
