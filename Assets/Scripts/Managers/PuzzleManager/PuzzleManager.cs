using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private Dictionary<GameObject,bool> isButtonPress = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        GameEvents.ButtonChangeStatus += OnButtonStatusChange;
    }

    private void OnDisable()
    {
        GameEvents.ButtonChangeStatus -= OnButtonStatusChange;
    }

    private void OnButtonStatusChange(GameObject buttonGameObject, bool isPress)
    {
        if(isButtonPress == null)
        {
            Debug.LogError("isButtonPress is null");
            return;
        }

        isButtonPress[buttonGameObject] = isPress;
        

        foreach (bool buttonPressStatus in isButtonPress.Values)
        {
            if (!buttonPressStatus) return;
        }
        
        //*)  if the code come here all the isButtonPress list are true!
        GameEvents.OnGameEnd();
    }

    void Awake()
    {
        Trigger[] buttonsGO = FindObjectsByType<Trigger>(FindObjectsSortMode.None);
        if(buttonsGO == null || buttonsGO.Length == 0)
        {
            Debug.LogError("can't find any triggers!");
            return;
        }
        foreach (Trigger trigger in buttonsGO)
        {
            isButtonPress[trigger.gameObject] = false;
        }
    }




}
