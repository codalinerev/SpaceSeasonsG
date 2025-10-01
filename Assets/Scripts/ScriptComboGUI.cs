using TMPro;
using UnityEngine;

public class ScriptComboGUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   public TMP_Text noCombo;
   public GameControler GameControler;
   

    // Update is called once per frame
   void Update()
    {
        noCombo.text = " Combo X " + GameControler.combo.ToString() + "\n" + "corect " + GameControler.corectMoves.ToString();
    }
    
}
