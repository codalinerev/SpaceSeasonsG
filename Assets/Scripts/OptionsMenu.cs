using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public int levelUser;

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void GotoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneMenu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LevelEasy()
    {
        levelUser = 1;
    }
    public void LevelMedium()
    {
        levelUser = 2;
    }
    public void LevelHard()
    {
        levelUser = 3;
    }
    
}
