using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public TMP_InputField InputName;
    public int levelUser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goToSceneEspace()
    {
        DataManager.levelUser = levelUser;
        SceneManager.LoadScene("SceneEspace");
    }

    public void goToOptions()
    {
        SceneManager.LoadScene("SceneOptions");
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
        SceneManager.LoadScene("SceneTutorial");
    }
    public void ReadName()
    {
        DataManager.playerName = InputName.text;
    }
    public void LevelEasy()
    {
        levelUser = 1;
        Debug.Log("level choisi " + levelUser);
    }
    public void LevelMedium()
    {
        levelUser = 2;
        Debug.Log("level choisi " + levelUser);
    }
    public void LevelHard()
    {
        levelUser = 3;
        Debug.Log("level choisi " + levelUser);
    }




}
