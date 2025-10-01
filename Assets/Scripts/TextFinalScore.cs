using UnityEngine;
using UnityEngine.UI;

public class TextFinalScore : MonoBehaviour
{
    public Text scoreFinal;

    void UpdateFinalScoreText()
    {
        scoreFinal.text = "FINAL SCORE : " + GameControler.Score.ToString();
    }

}
