using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    private DialogueController dialogueController;

    private void Start()
    {
        dialogueController = GetComponentInParent<DialogueController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            Debug.Log("Вошел в коллайдер");
            dialogueController.StartDialogue(dialogueLines);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            dialogueController.EndDialogue();
        }
    }
}

