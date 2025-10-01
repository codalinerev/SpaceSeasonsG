using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }// recommence le jeu
    public void Restart()
    {
        Debug.Log("Restart game with button in GameOver Scene");
        GameControler.state = GameControler.GameState.Start;
        SceneManager.LoadScene("GameScene");
    }
}
