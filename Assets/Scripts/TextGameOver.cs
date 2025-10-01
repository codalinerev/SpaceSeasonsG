using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextGameOver : MonoBehaviour
{
    public Text textGO;
    public TMP_Text GOReason;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textGO.text = " ";
        //textGO.text = "GAME OVER";
        GOReason.text = " ";
    }

    // Update is called once per frame
    void TextGO()
    {
        
    }
}
