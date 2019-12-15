using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTargetSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SmoothCameraFollow followCamera;
    [SerializeField] private List<GameObject> developerDescriptions;
    private int currentIndex = 0;
    private List<Transform> cameraSnapPoints = new List<Transform>();
    private void Awake()
    {
        foreach (Transform item in transform)
        {
            cameraSnapPoints.Add(item);
        }
    }
    private void Start()
    {
        followCamera.SetTarget(cameraSnapPoints[currentIndex]);
    }
    private void SetSnapPoint()
    {
        followCamera.SetTarget(cameraSnapPoints[currentIndex]);
        ToggleText(true, currentIndex);
    }

    private void ToggleText(bool toggle, int index)
    {
        developerDescriptions[index].SetActive(toggle);
    }

    private void OnShiftLeft()
    {
        ToggleText(false, currentIndex);
        if (currentIndex == 0)
        {
            currentIndex = developerDescriptions.Count - 1;
        }
        else
        {
            currentIndex--;
        }
        SetSnapPoint();
    }
    private void OnShiftRight()
    {
        ToggleText(false, currentIndex);
        if (currentIndex == developerDescriptions.Count - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        SetSnapPoint();
    }
    private void OnLeave()
    {
        SceneManager.LoadScene(0);
    }
}
