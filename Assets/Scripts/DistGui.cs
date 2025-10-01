using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistGui : MonoBehaviour
{
    public TMP_Text distText;
    public static GameControler GameControler;

    // actualize sur l'écran la distance parcourue par le personnage 
    void Update()
    {
        distText.text = "dist  " + GameControler.Distance.ToString() + "/" + GameControler.DistanceRun.ToString() + "m";
    }
    }

