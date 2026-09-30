using UnityEngine;

public class GameState : MonoBehaviour
{
    [field: SerializeField] public StateSO stateRules { get; private set; }
    [field: SerializeField] public GameState preivousState { get; set; }
    public bool wasJustEntered { get; set; }

}
