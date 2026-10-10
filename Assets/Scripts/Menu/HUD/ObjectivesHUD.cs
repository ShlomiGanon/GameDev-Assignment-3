using TMPro;
using UnityEngine;

public class ObjectivesHUD : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI objectivesText;

    [Header("Colors")]
    [SerializeField] private Color finishColor;
    [SerializeField] private Color amountUpColor;
    [SerializeField] private Color amountDownColor;
    [SerializeField] private Color emptyColor;

    private void Awake()
    {
        if (objectivesText == null)
        {
            objectivesText = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        ObjectivesEvents.ObjectivesManagerProgressChanged += OnObjectivesManagerProgressChanged;
    }

    private void OnDisable()
    {
        ObjectivesEvents.ObjectivesManagerProgressChanged -= OnObjectivesManagerProgressChanged;
    }

    private void OnObjectivesManagerProgressChanged(ObjectiveSet changedSet)
    {
        RefreshText(changedSet);
    }

    private void RefreshText(ObjectiveSet changedSet)
    {
        if (objectivesText == null || changedSet == null)
        {
            return;
        }

        int completedCount = changedSet.GetCompleteCount();
        int totalCount = completedCount + changedSet.GetNeedToCompleteCount();

        objectivesText.text = $"Objectives: {completedCount} / {totalCount}";
    }
}