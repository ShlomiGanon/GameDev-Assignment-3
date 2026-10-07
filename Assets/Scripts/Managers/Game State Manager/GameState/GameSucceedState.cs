using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSucceedState : GameState
{
    [SerializeField] string loadSceneName = "MainMenu";

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Befor load mainmenu");
        SceneManager.LoadScene(loadSceneName);
    }
}
