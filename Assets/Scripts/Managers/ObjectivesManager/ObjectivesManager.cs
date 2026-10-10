using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivesManager : MonoBehaviour
{
    [SerializeField] private bool reportStateOnStart = true;

    private ObjectiveSet objSet;
    private bool lastReportedCompleted;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (objSet == null)
        {
            InitializeLevelObjectives();
        }

        if (reportStateOnStart)
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
        UnsubscribeFromObjectiveSet();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        InitializeLevelObjectives();
        ReportInnerState();
    }

    private void OnObjectiveChanged(Objective obj)
    {
        if (obj != objSet)
        {
            return;
        }

        if (objSet.IsFail)
        {
            ReportLevelFailed();
            return;
        }

        ReportCompletionState();
    }

    private void OnInnerObjectiveChanged(Objective obj)
    {
        ReportInnerState();
    }

    private void InitializeLevelObjectives()
    {
        DestroyObjectiveSet();
        CreateObjectiveSet();

        lastReportedCompleted = false;

        objSet.SetTrackMode(ObjectiveSet.ObjectiveFilterMode.MandatoryOnly);
        objSet.SetObjectivesList(FindSceneObjectives());
    }

    private void CreateObjectiveSet()
    {
        objSet = gameObject.AddComponent<ObjectiveSet>();
        objSet.OnObjectiveSetChanged += OnObjectiveChanged;
        objSet.OnInnerObjectiveChanged += OnInnerObjectiveChanged;
    }

    private void DestroyObjectiveSet()
    {
        if (objSet == null)
        {
            return;
        }

        UnsubscribeFromObjectiveSet();
        Destroy(objSet);
        objSet = null;
    }

    private void UnsubscribeFromObjectiveSet()
    {
        if (objSet != null)
        {
            objSet.OnObjectiveSetChanged -= OnObjectiveChanged;
            objSet.OnInnerObjectiveChanged -= OnInnerObjectiveChanged;
        }
    }

    public void ReportLevelFailed()
    {
        LevelEvents.OnLevelFailed();
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
        ObjectivesEvents.OnObjectivesManagerProgressChanged(objSet);
    }

    private List<Objective> FindSceneObjectives()
    {
        List<Objective> result = new();
        foreach (Objective objective in FindObjectsByType<Objective>(FindObjectsSortMode.None))
        {
            if (objective is not ObjectiveSet)
            {
                result.Add(objective);
            }
        }

        return result;
    }
}
