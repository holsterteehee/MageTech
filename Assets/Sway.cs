using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmount = 10f; 
    public float swaySpeed = 1f;   

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + sway, transform.position.z);
        transform.position = newPosition;

        // Update the sorting order based on the Y position
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}