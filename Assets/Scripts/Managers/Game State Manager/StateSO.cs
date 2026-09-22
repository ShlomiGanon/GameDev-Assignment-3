using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "Scriptable Objects/StateSO")]
public class StateSO : ScriptableObject
{
    [field: SerializeField] public bool CanMove {get; private set;}
    [field: SerializeField] public bool CanPause { get; private set; }
}
