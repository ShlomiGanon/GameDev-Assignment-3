using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivesManager : MonoBehaviour
{  
    [SerializeField] private ObjectiveSet objSet = null;
    


    public void Reset()
    {
        InitializeLevelObjectives();
    }


    void Start()
    {
        InitializeLevelObjectives();
        objSet.OnObjectiveChanged += OnObjectiveChanged;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        objSet.OnObjectiveChanged -= OnObjectiveChanged;
    }


    void OnObjectiveChanged(Objective obj)
    {
        if (obj == objSet)
        {
            if(obj.IsCompleted)
            {
                ObjectivesEvents.OnObjectivesCompleted();
            }
            else
            {
                ObjectivesEvents.OnObjectivesIncompleted();
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        InitializeLevelObjectives();
    }

    private void InitializeLevelObjectives()
    {
        EnsureTrackerExists();
        Objective[] objectivesArray = FindObjectsByType<Objective>(FindObjectsSortMode.None);
        List<Objective> SenceObjectives = new ();
        foreach (Objective obj in objectivesArray)
        {
            if (obj != objSet && obj != null)//to ignore the tracker
            {
                SenceObjectives.Add(obj);
            }
        }
        objSet.SetTrackMode(ObjectiveSet.TrackMode.Mandatory_Only);
        objSet.SetObjectivesList(SenceObjectives);
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
}
