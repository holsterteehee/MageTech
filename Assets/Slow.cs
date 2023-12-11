using UnityEngine;

public class SlowMovingObject : MonoBehaviour
{
    // Speed of the object movement
    public float moveSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Move the object continuously
        Vector3 movement = new Vector3(1f, 0f, 0f); // Adjust the direction as needed
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}