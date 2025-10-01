using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScriptGameO : MonoBehaviour
{
    public TMP_Text bestPlayerText;
    public TMP_Text YourBestScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {
        bestPlayerText.text = DataManager.bestPlayerText;
        YourBestScore.text = DataManager.YourBestScore;
    }
    
}
