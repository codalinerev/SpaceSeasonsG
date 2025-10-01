using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipInSpace : MonoBehaviour
{
    public Vector3 pos;
    public TMP_Text message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = Vector3.zero;
        message.text = "Wait...It's going to start!";
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GoToGameAfter5Sec());
    }

    IEnumerator GoToGameAfter5Sec()
    {
        yield return new WaitForSeconds(5);
        GoToSceneGame();
    }

    public void GoToSceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
