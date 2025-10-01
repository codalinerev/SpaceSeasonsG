using UnityEngine;
using TMPro;

public class ScriptGUIDebug : MonoBehaviour
{
    public TMP_Text debug;
    public static GameControler GameControler;
    //public static DataManager Instance;


    // Update is called once per frame
    void Update()
    {
        debug.text = " currentCombo: " + GameControler.currentCombo.ToString()
         + "\n" + " ComboTimer: " + GameControler.comboTimer.ToString("F2")
        + "\n" + " Distance: " + GameControler.Distance.ToString()
        + "\n" + " Score: " + GameControler.Score.ToString()
        + "\n" + " FinalScore " + GameControler.ScoreFinal.ToString()
        + "\n" + " CorrectMoves: " + GameControler.corectMoves.ToString()
        + "\n" + " ObstaclesEvites: " + GameControler.obstaclesEvites.ToString()
        + "\n" + "Name " + DataManager.playerName;

        
    }
}
