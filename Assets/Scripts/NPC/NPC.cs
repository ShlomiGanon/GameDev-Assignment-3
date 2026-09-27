using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue dialogueData;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image canvasPortraitImage;
    [SerializeField] private TMP_Text buttonText;

    private bool isTalking;
    private bool isTyping = false;
    private bool toNextMessage = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTalking)
        {
            StartCoroutine(PlayDialogue());
        }
    }
    public void OnDialogueButtonClicked()
    {
        if (!isTalking)
            return;

        if(isTyping)
        {
            isTyping = false;
        }
        else
        {
            toNextMessage = true;
        }
    }
    private IEnumerator PlayDialogue()
    {
        isTalking = true;

        dialoguePanel.SetActive(true);
        nameText.SetText(dialogueData.NpcName);
        if (canvasPortraitImage != null)
        {
            canvasPortraitImage.sprite = dialogueData.NpcPortrait;
        }

        for(int i = 0; i < dialogueData.DialogueLines.Length ; i++) 
        {
            string message = dialogueData.DialogueLines[i];

            dialogueText.SetText("");

            isTyping = true;
            toNextMessage = false;

            buttonText.SetText("Skip");

            foreach (char letter in message)
            {
                if (!isTyping)
                    break;

                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData.TypingSpeed);
            }

            bool isLastMessage = i == dialogueData.DialogueLines.Length - 1;
            buttonText.SetText(isLastMessage ? "Close" : "Next");
            
            dialogueText.SetText(message);
            isTyping = false;

            yield return new WaitUntil(()=> toNextMessage);
        }

        dialogueText.SetText("");
        dialoguePanel.SetActive(false);

        isTalking = false;
    }
}