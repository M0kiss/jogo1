using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidePlatform : MonoBehaviour
{
    public List<PlatformEffector2D> Platforms;
    PlayerMovement groundCheck;
    public KeyCode downKey;
    //bool onPlatform = false; - Not currently being used
    GameObject Player;
    private void Start()
    {
        groundCheck = FindObjectOfType<PlayerMovement>();
    }
   
    private void Update()
    {
        if(groundCheck.CanJump() == true && UserInput.instance.playerController.InGame.Move.ReadValue<Vector2>().y < 0)
        {
            foreach (PlatformEffector2D plats in Platforms)
            {
                plats.surfaceArc = -180;
            }
            Invoke("PlataformaNormal", 0.7f);
        }
    }
    void PlataformaNormal()
    {
        foreach (PlatformEffector2D plats in Platforms)
        {
            plats.surfaceArc = 180;
        }
    }
}
