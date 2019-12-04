using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateUVScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float rotationSpeed;
    private RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rect.Rotate(new Vector3(0,0,1) * rotationSpeed);
    }
}
