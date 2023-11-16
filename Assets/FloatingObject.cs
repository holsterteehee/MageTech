using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatAmplitude = 0.5f; // Adjust the floating height.
    public float floatSpeed = 1.0f; // Adjust the floating speed.

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate a new Y position for the object based on a sine wave.
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Apply the new position to the object.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
