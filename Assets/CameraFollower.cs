using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's Transform component.
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset to control the camera's position relative to the player.

    void Update()
    {
        if (target != null)
        {
            // Calculate the target position by adding the offset to the player's position.
            Vector3 targetPosition = target.position + offset;

            // Set the camera's position to the target position.
            transform.position = targetPosition;
        }
    }
}
