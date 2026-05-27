using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upDownRotate : MonoBehaviour
{
    [Header("Set Rotating")]
    [Tooltip("Check this to Rotate")]
    public bool checkToRotate;
    public float rotateSpeed = 15.0f;

    [Header("Set Up/Down Motion")]
    [Tooltip("Check this to Float")]
    public bool checkToFloat;
    public float height = 0.5f;
    public float upDownSpeed = 1f;

    // Position Storage Variables
    Vector3 startingPosition = new Vector3();
    Vector3 tempPosition = new Vector3();

    void Start()
    {
        // Store the starting position & rotation of the object
        startingPosition = transform.position;
    }

    void Update()
    {
        // Rotate object around Y-Axis
        if (checkToRotate)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * rotateSpeed, 0f), Space.World);
        }

        // float object up and down
        if (checkToFloat)
        {
            tempPosition = startingPosition;
            tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * upDownSpeed) * height;
            transform.position = tempPosition;
        }   
    }
}