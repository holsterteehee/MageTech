using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;

    public GameObject fireballPrefab;

    public static SpawnManager Instance {
        get
        {
  
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SpawnManagerSingleton");
                    instance = singletonObject.AddComponent<SpawnManager>();
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        // Ensure there's only one instance, destroy any duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject SpawnFireball() {
        GameObject fireball = Instantiate(fireballPrefab, Vector3.zero, Quaternion.identity);
        return fireball;
    }

    public GameObject SpawnFireball(Transform spawn, Transform target) {
        // Get the direction the player is relative to the enemy.
        Vector3 dir = target.position - spawn.position;
        // Find the angle between the two vectors
        float angle = Mathf.Atan2(dir.y, dir.x);
        // Rotate the image based on that angle
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
        GameObject fireball = Instantiate(fireballPrefab, Vector3.zero, Quaternion.identity);
        fireball.transform.rotation = rotation;
        fireball.transform.position = spawn.position;
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dir.normalized.x * 15.0f, dir.normalized.y * 15.0f);
        Fireball fb = fireball.GetComponent<Fireball>();
        fb.target = target.position;
        return fireball;
    }
}
