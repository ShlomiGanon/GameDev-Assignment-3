using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Dialogue/NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [SerializeField] private string npcName;
    public string NpcName { get => npcName; private set => npcName = value; }
    [SerializeField] private Sprite npcPortrait;
    public Sprite NpcPortrait { get => npcPortrait; private set => npcPortrait = value; }

    [TextArea]
    [SerializeField] private string[] dialogueLines;
    public string[] DialogueLines { get => dialogueLines; private set => dialogueLines = value; }

    [SerializeField] private float typingSpeed = 0.05f;
    public float TypingSpeed { get => typingSpeed; private set => typingSpeed = value; }

    [SerializeField] private float messageDelay = 2f;
    public float MessageDelay { get => messageDelay; private set => messageDelay = value; }
}