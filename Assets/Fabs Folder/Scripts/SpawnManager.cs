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
}
