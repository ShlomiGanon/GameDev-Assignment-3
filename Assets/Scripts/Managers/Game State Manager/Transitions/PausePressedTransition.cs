using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePressedTransition : TransitionBase
{
    bool pausePressed = false;
    [SerializeField]
    List<string> menuSceneNames = new()
    {
        "MainMenu"
    };

    private void Start()
    {
        InputEvents.Pause += OnPausePressed;
    }

    private void OnPausePressed()
    {
        pausePressed = true;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = pausePressed && !menuSceneNames.Contains(SceneManager.GetActiveScene().name);
        ResetParameters();
        return shouldTransition;
    }

    public override void ResetParameters()
    {
        pausePressed = false;
    }

    private void OnDestroy()
    {
        InputEvents.Pause -= OnPausePressed;
    }
}