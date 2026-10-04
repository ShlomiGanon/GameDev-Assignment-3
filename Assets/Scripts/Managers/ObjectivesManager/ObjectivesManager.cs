using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivesManager : MonoBehaviour
{
    //we just add "... List<Objective> objectives ..." to be able to see on inspector objectives information
    [SerializeField] private List<Objective> mandatoryObjectives = new();
    
    [SerializeField] private HashSet<Objective> objectivesNeedToComplete = new();
    [SerializeField] private HashSet<Objective> objectivesCompleted = new();
    [field: SerializeField] public bool AllObjectivesCompleted { get; private set; }
    


    public void Reset()
    {
        BuildMandatoryObjectivesList();
    }

    void Awake()
    {
        
        //register to global objectives events
        ObjectivesEvents.objectiveStart += OnObjectiveStart;
        ObjectivesEvents.objectiveCompleted += OnObjectiveCompleted;
        ObjectivesEvents.objectiveUncompleted += OnObjectiveUnCompleted;
    }
    private void OnDestroy()
    {
        //unregister to global objectives events
        ObjectivesEvents.objectiveStart -= OnObjectiveStart;
        ObjectivesEvents.objectiveCompleted -= OnObjectiveCompleted;
        ObjectivesEvents.objectiveUncompleted -= OnObjectiveUnCompleted;
    }

    void Start()
    {
        InitializeLevelObjectives();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        InitializeLevelObjectives();
    }

    private void InitializeLevelObjectives()
    {
        BuildMandatoryObjectivesList();
        SyncObjectivesHashSets();
        CheckAndUpdateCompletionStatus();
    }

    private void OnObjectiveStart(Objective objective)
    {
        //this is will be can call in runtime (and the system will add the new dynamic objective)
        if (objective != null && !objective.IsCompleted && !objective.IsOptional)
        {
            objectivesNeedToComplete.Add(objective);
        }
    }

    // Update is called once per frame
    void OnObjectiveCompleted(Objective completedObjective)
    {
        if (objectivesNeedToComplete.Contains(completedObjective))
        {
            objectivesNeedToComplete.Remove(completedObjective);
            objectivesCompleted.Add(completedObjective);
        }
        CheckAndUpdateCompletionStatus();
    }

    void OnObjectiveUnCompleted(Objective uncompletedObjective)
    {
        if (objectivesCompleted.Contains(uncompletedObjective))
        {
            objectivesNeedToComplete.Add(uncompletedObjective);
            objectivesCompleted.Remove(uncompletedObjective);
        }
        CheckAndUpdateCompletionStatus();
    }

    void CheckAndUpdateCompletionStatus()
    {
        bool newStatus;
        if (objectivesNeedToComplete.Count == 0 && mandatoryObjectives.Count == objectivesCompleted.Count)
        {
            newStatus = true;
        }
        else
        {
            newStatus = false;       
        }

        //fire the events only if the status was change.
        if(newStatus != AllObjectivesCompleted)
        {
            AllObjectivesCompleted = newStatus;
            if (AllObjectivesCompleted) ObjectivesEvents.OnObjectivesCompleted();
            else ObjectivesEvents.OnObjectivesIncompleted();
        }
    }


    private void BuildMandatoryObjectivesList()
    {
        if (mandatoryObjectives == null) mandatoryObjectives = new();
        mandatoryObjectives.Clear();
        Objective[] objectivesArray = FindObjectsByType<Objective>(FindObjectsSortMode.None);
        foreach (Objective objective in objectivesArray)
        {
            if (objective.IsOptional == false)
            {
                mandatoryObjectives.Add(objective);
            }
        }
    }

    private void SyncObjectivesHashSets()
    {
        objectivesNeedToComplete.Clear();
        objectivesCompleted.Clear();

        foreach (Objective objective in mandatoryObjectives)
        {
            if (objective.IsCompleted)
            {
                objectivesCompleted.Add(objective);
            }
            else
            {
                objectivesNeedToComplete.Add(objective);
            }
        }
    }
}
