using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private int loadSceneIndex;

    private void OnChangeScene()
    {
        SceneManager.LoadScene(loadSceneIndex);
    }
}
