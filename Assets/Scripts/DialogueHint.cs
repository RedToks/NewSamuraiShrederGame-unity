    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class DialogueHint : MonoBehaviour
    {
        private TextMeshProUGUI hintText;
        private DialogueManager dialogueManager;
        [SerializeField] private float activationDistance = 3f;

        private void Start()
        {
            hintText = GetComponent<TextMeshProUGUI>();
            dialogueManager = FindObjectOfType<DialogueManager>();
            hintText.enabled = false;
        }
        private void Update()
        {
            float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

            if (distance <= activationDistance)
            {
                hintText.enabled = true;
            }
            else
            {
                hintText.enabled = false;
            }

            if (hintText.enabled && dialogueManager.DialogueActive)
            {
                hintText.enabled = false;           
            }       
        
        }
    }
