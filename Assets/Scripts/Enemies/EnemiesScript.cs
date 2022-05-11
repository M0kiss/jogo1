using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    public int maxHealth = 20;
    public int dano;
    public int currentHealth;
    public GameObject fxDie;
    public GameObject fxHit;
    public Transform whereToAddEffect;
    Transform customWhereToAdd;
    MyHealthSystem myHealthSystem;
    
    //public GameObject monster;
    void Start()
    {
        myHealthSystem = GameObject.FindObjectOfType<MyHealthSystem>();
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Instantiate(fxHit, whereToAddEffect.position, Quaternion.identity);
        Debug.Log("Damage!");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(fxDie, whereToAddEffect.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player") && dano > 0)
        {
            Debug.Log("Inimigo deu dano em player.");
            myHealthSystem.Dano(dano);
        }
    }
}