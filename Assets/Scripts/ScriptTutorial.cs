using System;
using System.Collections;
using System.IO;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScriptTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] groundsOnStage;
    [SerializeField] private GameObject[] groundsPrefabs;
    public TMP_Text EtapeText;
    public int Distance = 0; // Distance run

    public static int toCollect = 10;
    public int numberOfGrounds = 4;
    //public GameObject Ship;
    public Vector3 pos;
    [SerializeField] private float speed = 4f;
    private const float MIN_X = -15f;
    private const float MAX_X = 15f;
    public string messageDebug = " ";
    public string[] Messages;
    public KeyCode[] KeyCodes;
    public int i = 0;
    public KeyCode key;
    public string message;
    public TMP_Text recapText;

    void Start()
    {
        AfficheTextTuto();
        InitialiseTerrainAndHero();
        Messages = new string[] { "press left arrow to move left ", "press right arrow to move right", "press space bar to jump" };
        KeyCodes = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space };
    }
    void Update()
    {
        if (i < 3)
        {
            Debug.Log("i = " + i);
            key = KeyCodes[i];
            message = Messages[i];
            AfficheMessage(message);
            EtapeText.text = message;
            Autoplay();
            if (Input.GetKey(key))
            {
                Move(key);
                i++;
            }
        }
        else if (i == 3)
        {
            Debug.Log("i = " + i);
            i++;
            StartCoroutine(Wait2secRedescend());
        }
        else if (i == 5)
        {
            Debug.Log("i = " + i);
            EtapeText.text = " ";
            recapText.text = " Ship goes forward by itself" + "\n  press left arrow to go left \n  press right arrow to go right " +
            "\n press Space to jump \n press F to freeze the obstacle before you (3 times each level) \n click sur bonus icon to activate.";
            StartCoroutine(WaitNext());
            i++; // après 3 sec passage à la Scene Menu
            //SceneManager.LoadScene("SceneMenu");
        }
    }
    public void InitialiseTerrainAndHero()
    {
        //Ship = GameObject.Find("vehicle");
        //Debug.Log("ship found ");
        //Ship.transform.position = new Vector3(0, 0.4f, 0);
        transform.position = new Vector3(10, 0.6f, 20);
        float pos = transform.position.z;// + 10;
        groundsOnStage = new GameObject[numberOfGrounds];
        for (int i = 0; i < numberOfGrounds; i++)
        {
            //int randomIndex = UnityEngine.Random.Range(0, groundsPrefabs.Length);
            //GameObject ground = Instantiate(groundsPrefabs[randomIndex]);
            GameObject ground = Instantiate(groundsPrefabs[0]);
            ground.transform.position = new Vector3(0, 0.4f, pos);
            groundsOnStage[i] = ground;
            pos += 60;
        }
    }

    public void UpdateTerrain()
    {
        for (int i = numberOfGrounds - 1; i >= 0; i--)
        {
            GameObject ground = groundsOnStage[i];
            if (ground.transform.position.z + 30 < transform.position.z - 6)
            {
                float z = ground.transform.position.z;
                // Destroy(ground);
                //int randomIndex = UnityEngine.Random.Range(0, groundsPrefabs.Length);
                //ground = Instantiate(groundsPrefabs[randomIndex]);
                ground.transform.position = new Vector3(0, 0f, z + 60 * (numberOfGrounds));
                groundsOnStage[i] = ground;
            }
        }
    }

    public void AfficheMessage(string message)
    {
        Debug.Log("message: " + message);
    }

    public void Autoplay()
    {
        transform.position -= transform.forward * Time.deltaTime * 5f;
        Distance = (int)transform.position.z;
        UpdateTerrain();
        Debug.Log("autoplay");
    }
    public void AfficheTextTuto()
    {
        Debug.Log("Affichage du texte tutoriel");
    }
    public void Move(KeyCode key)
    {
        if (key == KeyCode.LeftArrow)
            transform.position = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
        else if (key == KeyCode.RightArrow)
            transform.position = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
        else if (key == KeyCode.Space)
        { transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z); }
    }
    IEnumerator WaitNext()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("SceneMenu");
    }
    public void AfficheMessage()
    {
        message = "";

    }

    IEnumerator Wait2secRedescend()
    {
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
        i++;    
    }

}
