using System;
using TMPro;
using UnityEngine;

public class ObjectivesHUD : MonoBehaviour
{
    [SerializeField] private ObjectiveSet objectiveSet;

    private TextMeshProUGUI textMeshPro;

    private int totalObjectives;
    private int completedObjectives;

    [SerializeField] private Color finishColor;
    [SerializeField] private Color amountUpColor;
    [SerializeField] private Color amountDownColor;
    [SerializeField] private Color emptyColor;
    private void Start()
    {
        if(objectiveSet == null)
        {
            Debug.LogError("you have not assing ObjectiveSet!");
        }
        else
        {
            objectiveSet.OnObjectiveChanged += OnObjectiveChanged;
        }
    }

    private void OnDestroy()
    {
        if (objectiveSet != null)
        {
            objectiveSet.OnObjectiveChanged -= OnObjectiveChanged;
        }
    }

    private void OnObjectiveChanged(ObjectiveSet objective)
    {
        if(objective != null && objective == objectiveSet)
        {

        }
    }
}
