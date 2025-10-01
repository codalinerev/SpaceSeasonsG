using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuCheckPoint : MonoBehaviour
{
    public TMP_Text toCollect;
    public TMP_Text collected;
    public TMP_Text messageCheck;
    public string message;
    public string nextScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"scene {SceneManager.GetActiveScene().name}");
        toCollect.text = GameControler.toCollect.ToString();
        collected.text = GameControler.collected.ToString();
        //GameControler.state = GameControler.GameState.Wait3Sec;
        //StartCoroutine(Message3Sec());
        StartCoroutine(ChangeScene3Sec());

    }

    // Update is called once per frame
    IEnumerator Message3Sec()
    {
        yield return new WaitForSeconds(3);
        if (GameControler.toCollect > GameControler.collected) message = "not enough presents...";
        else message = $"{GameControler.collected} presents collected :  Next Level ";
        messageCheck.text = message;
    }
    IEnumerator ChangeScene3Sec()
    {
        Debug.Log("debut coroutine wait 3 sec");
        yield return new WaitForSeconds(3);
        if (GameControler.toCollect > GameControler.collected)
        {
            message = "not enough presents...";
            nextScene = "GameOverScene";
            GameControler.state = GameControler.GameState.GameOver;
            Debug.Log("check point go to game over");
        }
        else
        {
            message = $"{GameControler.collected} presents collected :  Next Level ";
            GameControler.Level += 1;
            GameControler.state = GameControler.GameState.NewLevel;
            Debug.Log($"CheckPointScene start; state {GameControler.state}");
            nextScene = "GameScene";
        }
        Debug.Log($"scene CheckPoint, go to next scene {nextScene}, state : {GameControler.state}");
        SceneManager.LoadScene(nextScene);
    }
    
}
