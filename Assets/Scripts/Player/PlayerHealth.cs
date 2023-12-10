using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int Health;
    public int MaxHealth;
    private Knockback knockback;
    private Flash flash;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private GameObject deathVFXPrefab;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(Transform enemy, int dmg = 1) { 
        Health -= dmg;
        StartCoroutine(GetKnocked(enemy, knockBackThrust));
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator GetKnocked(Transform enemy, float knockBackThrust) {
        PlayerController pc = GetComponent<PlayerController>();
        if(pc) pc.enabled = false;
        knockback.GetKnockedBack(enemy.transform, knockBackThrust);
        yield return new WaitForSeconds(.2f);
        if (pc) pc.enabled = true;
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (Health <= 0)
        {
            //Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
