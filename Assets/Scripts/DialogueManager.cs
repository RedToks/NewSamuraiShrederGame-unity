using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public List<GameObject> characterObjects;
    private int currentCharacterIndex = 0;

    private int currentLine = 0;
    private TextMeshProUGUI dialogueText;
    private DialogueTrigger dialogueTrigger;
    [SerializeField] private Button button;


    public bool DialogueActive { get; private set; } = false;

    private void Start()
    {
        dialogueText = GetComponent<TextMeshProUGUI>();
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        dialogueText.enabled = false;
    }
    private void Update()
    {
        if (DialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            ContinueDialogue();
        }
    }

    public void StartDialogue(int characterIndex)
    {
        DialogueActive = true;
        currentCharacterIndex = characterIndex;
        currentLine = 0;
        dialogueText.enabled = true;

        if (currentCharacterIndex < characterObjects.Count && currentLine < characterObjects[currentCharacterIndex].GetComponent<DialogueCharacter>().dialogueLines.Length)
        {
            DisplayLine();
        }

        if (button != null) 
        {
            button.gameObject.SetActive(false);
        }
        
    }

    private void DisplayLine()
    {
        dialogueText.text = characterObjects[currentCharacterIndex].GetComponent<DialogueCharacter>().dialogueLines[currentLine];
    }

    private void ContinueDialogue()
    {
        currentLine++;

        if (currentCharacterIndex < characterObjects.Count && currentLine < characterObjects[currentCharacterIndex].GetComponent<DialogueCharacter>().dialogueLines.Length)
        {
            DisplayLine();
        }
        else
        {
            EndDialogue();          
        }
    }

    public void EndDialogue()
    {
        if (button != null)
        {
            button.gameObject.SetActive(true);
        }
        DialogueActive = false;
        dialogueText.enabled = false;
        dialogueTrigger.Sprite.enabled = false;
    }
}
