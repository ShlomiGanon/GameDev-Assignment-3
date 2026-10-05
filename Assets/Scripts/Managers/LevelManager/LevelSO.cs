using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [field: SerializeField] public string LevelName { get; private set; }
    [field: SerializeField] public string SceneName { get; private set; }

}
