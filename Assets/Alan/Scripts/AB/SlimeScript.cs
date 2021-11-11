using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int dano = 5;
    public float speed = 5;
    public SpriteRenderer SR_slime;
    public BoxCollider2D Boxcollider;
    public CircleCollider2D Circlecollider;
    public MyHealthSystem healthSystem;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("BATEU ");
            healthSystem.Dano(dano);
        }
    }
   /* public void MudarColi()
    {
        Circlecollider.enabled = true;
    }
    public void VoltarColi()
    {
        Circlecollider.enabled = false;
    }*/
}