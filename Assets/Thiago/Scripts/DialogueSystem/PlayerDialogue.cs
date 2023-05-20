using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUi;
    public DialogueUI DialogueUI => dialogueUi;
    public Interectible interectible { get; set; }

    private Moving move;

    private void Start()
    {
        move = GetComponentInParent<Moving>();
    }

    private void Update()
    {
        if (UserInput.instance.playerController.InGame.Debug_E.triggered)
        {
            if (interectible != null && !dialogueUi.isOpen)
            {
                interectible.Interect(this);
            }
        }        
    }

    private bool canMoveAgain;
    private void FixedUpdate()
    {
        if (dialogueUi.isOpen)
        {
            canMoveAgain = true;
            move.stopMove();
        }
        else if(!dialogueUi.isOpen && canMoveAgain)
        {
            move.canMove = true;
            canMoveAgain = false;
        }
    }
}