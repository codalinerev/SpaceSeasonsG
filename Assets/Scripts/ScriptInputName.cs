using UnityEngine;
using TMPro;
using System.IO;

public class ScriptInputName : MonoBehaviour
{
    public static string playerName;
    public TMP_InputField inputName;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetPlayerName()
    {
        playerName = inputName.text;
        Debug.Log("name set to " + playerName);
    }

}
