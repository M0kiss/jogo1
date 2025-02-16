using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ele ataca depois de jogar o objeto.

public class HoldAndThrow : MonoBehaviour
{
    #region Vari�veis de Funcionamento
    GameObject grabDetect;
    GameObject boxHolder;
    public float rayDist = 2;
    LayerMask ObjectLayer;
    public string Estado = "Normal";

    public float force = 7000;
    public float attackRate = 2f;
    public float NextattackTime = 0f;

    Animator PlayerAni;

    #endregion

    #region Vari�veis do objeto

    GameObject objectGO;
    GameObject FixedGO;
    PolygonCollider2D ObjectPoly;

    #endregion

    #region Vari�veis Caixas
    public  List<Rigidbody2D> Boxes;
    #endregion

    #region Start
    private void Start()
    {
        PlayerAni = GetComponentInChildren<Animator>();
        grabDetect = GameObject.Find("GrabDetect");
        boxHolder = GameObject.Find("GrabHolder");
        ObjectLayer = LayerMask.GetMask("ObjectLayer");
    }
    #endregion

    #region Update
    private void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.transform.position, grabDetect.transform.right, rayDist, ObjectLayer);
        Debug.DrawRay(grabDetect.transform.position, grabDetect.transform.right * rayDist, Color.green);
        if (grabCheck.collider != null && grabCheck.collider.tag == "Object")
        {
            objectGO = grabCheck.collider.gameObject;

            if (UserInput.instance.playerController.InGame.Attack.triggered && Estado != "Segurando")
            {
                Situa��o("Segurando");
;           }
        }
        if (Time.time >= NextattackTime && Time.time >= NextattackTime)
        {
            if (Estado == "Segurando" && UserInput.instance.playerController.InGame.Attack.triggered)
            {
                Jogar();
            }
        } 
    }
    #endregion

    #region M�todos
    void Situa��o(string estado)
    {
        switch (estado)
        {
            case "Segurando":
                {
                    Estado = "Segurando";
                    ActSitua��o("Segurar");
                }
                break;

            case "Normal":
                {
                    Estado = "Normal";
                }
                break;
        }
    }
   public void ActSitua��o(string estado)
   {
        switch (estado)
        {
            case "Segurar":
                {
                    FixedGO = objectGO;
                    objectGO.transform.SetParent(boxHolder.transform);
                    objectGO.transform.rotation = new Quaternion(objectGO.transform.rotation.x, 0,
                        objectGO.transform.rotation.z, 0);
                    objectGO.transform.position = boxHolder.transform.position;
                    objectGO.GetComponent<Rigidbody2D>().isKinematic = true;
                    objectGO.GetComponent<PolygonCollider2D>().enabled = false;
                    PlayerAni.SetBool("Holding", true);
                    NextattackTime = Time.time + 1f / attackRate;
                }
                break;
        }
   }

    void Jogar()
    {
        Debug.Log("Entrou em jogar");
        FixedGO.GetComponent<PolygonCollider2D>().enabled = true;
        FixedGO.transform.parent = null;
        FixedGO.transform.rotation = new Quaternion(objectGO.transform.rotation.x, 0,
            objectGO.transform.rotation.z, 0);
        FixedGO.GetComponent<Rigidbody2D>().isKinematic = false;
        FixedGO.GetComponent<Rigidbody2D>().AddForce(boxHolder.transform.right * force);
        FixedGO.GetComponent<Rigidbody2D>().AddForce(boxHolder.transform.up * 1000);
        Invoke("EstadoNormal", 0 * Time.fixedDeltaTime);

        PlayerAni.SetBool("Holding", false);
    }
    void EstadoNormal()
    {
        Estado = "Normal";
    }
    #endregion
}
