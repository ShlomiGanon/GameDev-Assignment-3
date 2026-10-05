
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivesManager : MonoBehaviour
{  
    [SerializeField] private ObjectiveSet objSet;


    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
    void OnSceneLoaded(Scene s, LoadSceneMode m) => InitializeLevelObjectives();

    void OnDestroy()
    {
        if (objSet != null) objSet.OnObjectiveChanged -= OnObjectiveChanged;
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


    void InitializeLevelObjectives()
    {
        EnsureTrackerExists();
        objSet.OnObjectiveChanged -= OnObjectiveChanged;
        objSet.OnObjectiveChanged += OnObjectiveChanged;
        objSet.SetTrackMode(ObjectiveSet.TrackMode.Mandatory_Only);
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

    List<Objective> FindSceneObjectives()
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
    void FindObjectivesInEditor()
    {
        if (objSet == null) objSet = GetComponent<ObjectiveSet>();
        if (objSet == null) objSet = UnityEditor.Undo.AddComponent<ObjectiveSet>(gameObject);

        objSet.SetObjectivesList(FindSceneObjectives());
        UnityEditor.EditorUtility.SetDirty(objSet);
    }
#endif


}