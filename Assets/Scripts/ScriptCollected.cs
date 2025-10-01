using UnityEngine;
using UnityEngine.UI;

public class ScriptCollected : MonoBehaviour
{
  //actualize sur l'écran le nombre de cadeaux collectés
    
   public Text noCollected;
   public GameControler GameControler;
    //public static int collected;
    //public static int toCollect;

    // Update is called once per frame
   void Update()
    {
      noCollected.text = GameControler.collected.ToString() + " / " + GameControler.toCollect.ToString();
    }
    }

