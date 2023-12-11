using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public void Update()
    {
        if (target != null) {
            if (target == transform.position) {
                Destroy(this);
                return;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
