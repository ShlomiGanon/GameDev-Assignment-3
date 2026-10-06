using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelSO> levelsData;
    private LevelSO currentLevel;

    private bool HasNextLevel()
    {
        int currentLevelIndex = levelsData.IndexOf(currentLevel);

        return currentLevelIndex >= 0 && currentLevelIndex < levelsData.Count - 1;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = levelsData.Find(level => level.SceneName == scene.name);
        Debug.Log($"{currentLevel}");
    }

    private LevelSO GetNextLevel()
    {
        if(HasNextLevel())
        {
            int currentLevelIndex = levelsData.IndexOf(currentLevel);
            int nextLevelIndex = currentLevelIndex + 1;
            return levelsData[nextLevelIndex];
        }
        else
        {
            Debug.Log("Theres no next level.");
            return null;
        }
    }

    public void LoadNextLevel()
    {
        LevelSO nextLevel = GetNextLevel();
        if(nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel.SceneName);
        }
    }

    public void CheckForNextLevel()
    {
        if (HasNextLevel())
        {
            LevelEvents.OnHasNextLevel();
        }
        else
        {
            LevelEvents.OnHasNoNextLevel();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        LevelEvents.loadNextLevel += LoadNextLevel;
        LevelEvents.checkForNextLevel += CheckForNextLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LevelEvents.loadNextLevel -= LoadNextLevel;
        LevelEvents.checkForNextLevel -= CheckForNextLevel;
    }
}
