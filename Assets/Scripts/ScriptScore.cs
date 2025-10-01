using UnityEngine;
using UnityEngine.UI;

public class ScriptScore : MonoBehaviour
{
    //actualize le score affiché sur la GUI
    
    public Text scoreText;
    public static int Score;
    public int Level;
    public static GameControler Instance;

    // Update is called once per frame
    public void Start()
    {
        //scoreText = ;
    }
    public void Update()
    {
        scoreText.text = " Score: " + GameControler.Score.ToString() + "  Level: " + GameControler.Level.ToString();
    }
}
 