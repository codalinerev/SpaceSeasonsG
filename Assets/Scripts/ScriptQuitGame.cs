using UnityEngine;

public class ScriptQuitGame : MonoBehaviour
{
    public int paused = 1;
    public void Quit()
    {
        Application.Quit();
        Debug.Log("quitting");
 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
#endif
    }
    
    public void Pause()
    {
        paused = 1 - paused;
        Time.timeScale = paused; // Pause the game
        Debug.Log("Game Paused");
    }
}

