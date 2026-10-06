using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivesManager : MonoBehaviour
{
    [SerializeField] private ObjectiveSet objSet;
    [SerializeField] private bool reportStateOnStart = true;

    private bool lastReportedCompleted;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (reportStateOnStart && objSet != null)
        {
            ReportCompletionState();
            ReportInnerState();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (objSet != null)
        {
            objSet.OnObjectiveSetChanged -= OnObjectiveChanged;
            objSet.OnInnerObjectiveChanged -= OnInnerObjectiveChanged;
        }
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        InitializeLevelObjectives();
    }

    private void OnObjectiveChanged(Objective obj)
    {
        if (obj == objSet)
        {
            ReportCompletionState();
        }
    }

    private void OnInnerObjectiveChanged(Objective obj)
    {
        ReportInnerState();
    }

    private void ReportCompletionState()
    {
        //never report the same completion state twice in a row.
        if (objSet.IsCompleted == lastReportedCompleted)
        {
            return;
        }

        lastReportedCompleted = objSet.IsCompleted;

        if (lastReportedCompleted)
        {
            ObjectivesEvents.OnObjectivesCompleted();
        }
        else
        {
            ObjectivesEvents.OnObjectivesIncompleted();
        }
    }

    private void ReportInnerState()
    {
        ObjectivesEvents.OnObjectivesManagerUpdatedObjectiveSet(objSet);
    }

    private void InitializeLevelObjectives()
    {
        EnsureTrackerExists();
        objSet.OnObjectiveSetChanged -= OnObjectiveChanged;
        objSet.OnObjectiveSetChanged += OnObjectiveChanged;
        objSet.OnInnerObjectiveChanged -= OnInnerObjectiveChanged;
        objSet.OnInnerObjectiveChanged += OnInnerObjectiveChanged;
        objSet.SetTrackMode(ObjectiveSet.ObjectiveFilterMode.MandatoryOnly);
        objSet.SetObjectivesList(FindSceneObjectives());
    }

    private void EnsureTrackerExists()
    {
        if (objSet == null)
        {
            objSet = GetComponent<ObjectiveSet>();
            if (objSet == null)
            {
                objSet = gameObject.AddComponent<ObjectiveSet>();
            }
        }
    }

    private List<Objective> FindSceneObjectives()
    {
        List<Objective> result = new();
        foreach (var o in FindObjectsByType<Objective>(FindObjectsSortMode.None))
        {
            if (o != objSet) result.Add(o);
        }
        return result;
    }

#if UNITY_EDITOR
    [ContextMenu("Find Objectives")]
    private void FindObjectivesInEditor()
    {
        if (objSet == null) objSet = GetComponent<ObjectiveSet>();
        if (objSet == null) objSet = UnityEditor.Undo.AddComponent<ObjectiveSet>(gameObject);

        objSet.SetObjectivesList(FindSceneObjectives());
        UnityEditor.EditorUtility.SetDirty(objSet);
    }
#endif
}