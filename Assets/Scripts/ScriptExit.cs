using UnityEngine;

public class ScriptExit : MonoBehaviour
{
    //*** cherche et détruit les objets qui passent en arrière du personnage ******************************
    GameObject player;
    void Start()
    {
        player = GameObject.Find("ShipObject");
    }

    void Update()
    {
        if (transform.position.z < player.transform.position.z - 20)
        {
            Destroy(gameObject);
        }
    }
}
