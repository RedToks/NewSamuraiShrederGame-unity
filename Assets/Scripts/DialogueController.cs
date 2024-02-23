using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private bool isDialogueActive = false;
    private List<string> dialogueLines = new List<string>();
    private int currentLine = 0;

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isDialogueActive)
        {
            ShowNextLine();
        }
    }

    public void StartDialogue(string[] newDialogueLines)
    {
        dialogueLines = new List<string>(newDialogueLines);
        currentLine = 0;
        ShowNextLine();
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
    }


    private void ShowNextLine()
    {
        if (currentLine < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
