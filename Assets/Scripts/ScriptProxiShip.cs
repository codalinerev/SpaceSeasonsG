using UnityEngine;

public class ScriptProxiShip : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     GameControler GameControler;
    void Start()
    {
        GameControler = GameControler.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit(Collider objetEvite)
    {
        if (objetEvite.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("ProxiShip: obstacle évité");
            GameControler.corectMoves++;
            Debug.Log("correctMoves: " + GameControler.corectMoves);
            GameControler.obstaclesEvites++;
        }
    }
}
