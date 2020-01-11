using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

public class ImportManager : MonoBehaviour
{
    #region Singleton Implementation
    private ImportManager() { }
    private static ImportManager instance;
    public static ImportManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ImportManager>();
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
        Debug.Log(Settings.ToString());
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

    [SerializeField] private float playerSpeedMultiplier = 1; //TODO
    public float PlayerSpeedMultiplier
    {
        get { return playerSpeedMultiplier; }
        set { playerSpeedMultiplier = value; }
    }

    [SerializeField] private float jumpHeightMultiplier = 1; //TODO
    public float JumpHeightMultiplier
    {
        get { return jumpHeightMultiplier; }
        set { jumpHeightMultiplier = value; }
    }

    [SerializeField] private float shoveForceMultiplier = 1; // TODO
    public float ShoveForceMultiplier
    {
        get { return shoveForceMultiplier; }
        set { shoveForceMultiplier = value; }
    }

    [SerializeField] private float shoveRangeMultiplier = 1; // TODO
    public float ShoveRangeMultiplier
    {
        get { return shoveRangeMultiplier; }
        set { shoveRangeMultiplier = value; }
    }

    [SerializeField] private float rotationMultiplier = 1; // TODO
    public float RotationMultiplier
    {
        get { return rotationMultiplier; }
        set { rotationMultiplier = value; }
    }
    [SerializeField] private float runAndJumpModifier = 1; // TODO
    public float RunAndJumpModifier
    {
        get { return runAndJumpModifier; }
        set { runAndJumpModifier = value; }
    }
    [SerializeField] private float chickenShootoutSpeed = 1; // TODO
    public float ChickenShootoutSpeed
    {
        get { return chickenShootoutSpeed; }
        set { chickenShootoutSpeed = value; }
    }
    public override string ToString()
    {
        return FinaleScoreMultiplier + "" + PlayerSpeedMultiplier;
    }
}
