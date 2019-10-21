using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private int loadSceneIndex;
    
    private void OnSwitchScene()
    {
        SceneManager.LoadScene(loadSceneIndex);
    }
}
