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

    private bool isTalking;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTalking)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private IEnumerator PlayDialogue()
    {
        GameEvents.OnDialogueStart();
        isTalking = true;

        dialoguePanel.SetActive(true);
        nameText.SetText(dialogueData.NpcName);
        if (canvasPortraitImage != null)
        {
            canvasPortraitImage.sprite = dialogueData.NpcPortrait;
        }

        foreach (string message in dialogueData.DialogueLines)
        {
            dialogueText.SetText("");

            foreach (char letter in message)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData.TypingSpeed);
            }

            yield return new WaitForSeconds(dialogueData.MessageDelay);
        }

        dialogueText.SetText("");
        dialoguePanel.SetActive(false);

        isTalking = false;
        GameEvents.OnDialogueEnd();
    }
}