using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    PlayerHealth PlayerHP;

    private void Awake() 
    {
        m_transform = GetComponent<Transform>();
        PlayerHP = FindObjectOfType<PlayerHealth>();
    }
    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right, 200, 13);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startpos, Vector2 endpos)
    {
        m_lineRenderer.SetPosition(0, startpos);
        m_lineRenderer.SetPosition(1, endpos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerHP.Hit(2);
        }
    }
}
