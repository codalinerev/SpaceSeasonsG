using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenuButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goToSceneGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void goToOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneOptions");
    }

    public void goToQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
#endif
    }
    public void GotoSceneTutorial()
    {
        Debug.Log("current scene: Accueil");
        Debug.Log("next clicked goto Tutorial");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTutorial");
    }
    public void GoToSceneMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneMenu");
    }
}

