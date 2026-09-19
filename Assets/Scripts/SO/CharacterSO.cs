using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    [Header("Info")]
    [SerializeField] string characterName = "Name";
    public string CharacterName { get => characterName; private set => characterName = value;}

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;    
    public float MoveSpeed { get => moveSpeed;  private set => moveSpeed = value;}
    [SerializeField] float jumpForce = 8f;
    public float JumpForce { get => jumpForce;  private set => jumpForce = value;}
    
    [Header("Health")]
    [SerializeField] int fullHealth = 1;
    public int FullHealth { get => fullHealth; private set => fullHealth = value;}

}
