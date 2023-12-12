using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform respawnPoint;
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
        StartCoroutine(IFrame());
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

    private IEnumerator IFrame() {
        var bc = GetComponent<BoxCollider2D>();
        if (bc) bc.enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (bc) bc.enabled = true;
    }
    
    public void DetectDeath()
    {
        if (Health <= 0)
        {
            //Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
           Respawn();
        }
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Health = MaxHealth;
        }
        else
        {
            print("Respawn not set.");
            //Debug.LogError("Respawn point not assigned to Damageable script.");
        }
    }
}
