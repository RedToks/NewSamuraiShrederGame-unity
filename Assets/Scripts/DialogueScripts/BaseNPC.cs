using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseNPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected TextMeshProUGUI dialogueText;
    [SerializeField] protected string[] dialogues;
    protected int currentDialogueIndex = 0;
    protected bool isInteracting = false;
    protected float startTime;

    [SerializeField] private float dialogueCloseDistance = 5f;

    public event Action OnLastDialogueFinished; 

    protected virtual void Start()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
        dialogueText.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (!isInteracting)
        {
            StartDialogue();
        }
        else
        {
            ContinueDialogue();
        }
    }

    protected void StartDialogue()
    {
        isInteracting = true;
        dialogueText.gameObject.SetActive(true);
        startTime = Time.time;

        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            StopDialogue();
        }
    }

    protected void ContinueDialogue()
    {
        if (currentDialogueIndex + 1 < dialogues.Length)
        {
            currentDialogueIndex++;
            dialogueText.text = dialogues[currentDialogueIndex];
            startTime = Time.time;
        }
        else
        {
            StopDialogue();
        }
    }

    protected void StopDialogue()
    {
        dialogueText.gameObject.SetActive(false);
        isInteracting = false;
        currentDialogueIndex = 0;

        OnLastDialogueFinished?.Invoke();
    }

    protected void Update()
    {
        float distance = Vector3.Distance(transform.position, Singletone.Instance.transform.position);

        if (isInteracting && (distance > dialogueCloseDistance || Time.time - startTime > 10f))
        {
            StopDialogue();
        }
    }
}
