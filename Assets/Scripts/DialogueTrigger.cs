using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int characterIndex;
    private DialogueManager dialogueManager;
    private bool canInteract = false;
    private bool isInteracting = false;
    [SerializeField] private float activationDistance = 3f;
    public SpriteRenderer Sprite { get; private set; }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (distance <= activationDistance)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
            isInteracting = false;
        }
        if (canInteract && Input.GetKeyDown(KeyCode.F) && !isInteracting)
        {
            isInteracting = true;
            Sprite.enabled = true;
            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(characterIndex);
            }
            else
            {
                Debug.LogError("DialogueManager is not assigned in the inspector.");
            }
        }
    }
}
