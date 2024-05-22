using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [TextArea] [SerializeField] private string[] dialogues;

    private int currentDialogueIndex = 0;
    private bool isInteracting = false;
    private float startTime;

    [SerializeField] private float dialogueCloseDistance = 5f;

    public event Action OnLastDialogueFinished;

    private TextAppearAnimation textAppearAnimation; 

    private void Start()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
        dialogueText.gameObject.SetActive(false);

        textAppearAnimation = dialogueText.GetComponent<TextAppearAnimation>();
    }

    public virtual void Interact()
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

    private void StartDialogue()
    {
        isInteracting = true;
        dialogueText.gameObject.SetActive(true);
        startTime = Time.time;

        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];

            textAppearAnimation.PlayAnimation();
        }
        else
        {
            StopDialogue();
        }
    }

    private void ContinueDialogue()
    {
        if (currentDialogueIndex + 1 < dialogues.Length)
        {
            currentDialogueIndex++;
            dialogueText.text = dialogues[currentDialogueIndex];
            startTime = Time.time;

            textAppearAnimation.PlayAnimation();
        }
        else
        {
            StopDialogue();
        }
    }

    private void StopDialogue()
    {
        dialogueText.gameObject.SetActive(false);
        isInteracting = false;
        currentDialogueIndex = 0;

        OnLastDialogueFinished?.Invoke();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Singletone.Instance.transform.position);

        if (isInteracting && (distance > dialogueCloseDistance || Time.time - startTime > 10f))
        {
            StopDialogue();
        }
    }
}
