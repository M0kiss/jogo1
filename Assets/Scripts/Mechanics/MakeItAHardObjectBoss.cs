using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(TrailRenderer))]
public class MakeItAHardObjectBoss : MonoBehaviour
{
    #region BasicComponents
    GameObject MyGOB;
    GameObject Player;
    HoldAndThrow holdAndThrow;
    AudioSource audioSource; 
    public AudioClip clip;
    public GameObject ExplodeFx;
    public int AttackDamage;


    private void Awake()
    {
        MyGOB = this.gameObject;
        audioSource = GetComponent<AudioSource>();
        Player = GameObject.Find("PlayerManager");
        holdAndThrow = Player.GetComponent<HoldAndThrow>();
    }
    private void Start()
    {
        MyGOB.tag = "Object";
        MyGOB.layer = 14;
        MyGOB.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;
    }

    private void Update()
    {
        transform.rotation = new Quaternion(transform.transform.rotation.x, 0,
            transform.transform.rotation.z, 0);
        transform.localScale = new Vector3(0.05f, 0.05f, 1);

    }

    #endregion
    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss" && holdAndThrow.Estado != "Segurando")
        {
            collision.GetComponent<BossScript>().TakeDamageByItem(AttackDamage);
            Destroy(gameObject);

        }
        else if(collision.tag == "Enemies" && holdAndThrow.Estado != "Segurando")
        {
            if(GetComponent<Rigidbody2D>().velocity.magnitude != 0)
            {
                collision.GetComponent<EnemiesScript>().TakeDamage(AttackDamage);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boss" && holdAndThrow.Estado != "Segurando")
        {
            collision.collider.GetComponent<BossScript>().TakeDamageByItem(AttackDamage);
            Destroy(gameObject);


        }
        // Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 20)
        {
            if (gameObject != null)
            {
                audioSource.pitch = collision.relativeVelocity.magnitude/30;                
                audioSource.PlayOneShot(clip, (collision.relativeVelocity.magnitude / 30));
            }
        }
    }
    #endregion
}
