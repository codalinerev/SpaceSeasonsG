using System;
using System.Collections;
using System.IO;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public static GameControler Instance;
    public static DataManager DataManager;
    public int ScoreFinal = 0;
    public Text textGO;//** explique pourquoi GameOver **********************
    public Text scoreFinal;
    public TMP_Text GOReason;
    public TMP_Text bonusType;
    public TMP_Text bonusState;
    public int levelUser;

    public static bool GameOver;
    public int var = 0;
    public float lastSpeed;
    public float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject[] groundsOnStage;
    [SerializeField] private GameObject CheckPointPrefab;
    [SerializeField] private GameObject[] groundsPrefabs;
    [SerializeField] private GameObject[] bonusPrefabs;
    [SerializeField] private GameObject[] cadeauPrefabs;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private UnityEngine.UI.Image HealthRed;
    [SerializeField] private UnityEngine.UI.Image HealthGreen;
    public static int Level = 1; // Number of Round is also the level
    public static int Distance = 0; // Distance run
    public static int lastDistance = 0;
    public static int DistanceRun = 1000; // one Round
    public string message = "azerty";
    public enum GameState { Start, Play, CheckPoint, GameOver, Pause, NewLevel, Wait }
    public static GameState state = GameState.NewLevel;

    public int corectMoves = 0;
    public int obstaclesEvites = 0;
    public static int etatVehicule = 3;
    public static int Score = 0;
    public static int collected = 0; // number of collected presents
    public static GameObject bonusCollected;
    public static int toCollect = 10;
    public static int noCadeaux;
    public static int noObstacles;
    public static int noBonus;
    public int combo = 1; // combo for bonus
    public int currentCombo;
    public float comboTimer = 0f;
    public int health;
    public int numberOfGrounds = 5;
    public Vector3 posCheckPoint = new Vector3(0, 0, DistanceRun);
    public GameObject Ship;
    //public GameObject bonus;
    public GameObject obstacle;
    public GameObject cadeau;
    public Vector3 pos;
    public float posZ;
    //public enum BonusType { Speed, Health, Invulnerable, Magnetic, Repair };
    public bool isPaused = false;
    public bool newLevel = false;
    public BonusClass bonusGC;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        Debug.Log("Awake GameController");
    }

    void Start()
    {
        state = GameState.Start;
        Debug.Log($"start game controler, state {state}");
        
    }
    void InitTerrain()
    {
        Debug.Log("init terrain");
        groundsOnStage = new GameObject[numberOfGrounds];
        for (int i = 0; i < numberOfGrounds; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, groundsPrefabs.Length);
            //Debug.Log("in for i>numberOfGrounds");
            GameObject ground = Instantiate(groundsPrefabs[randomIndex]);
            //GameObject ground = Instantiate(groundsPrefabs[0]);
            ground.transform.position = new Vector3(0, 0.4f, i * 60);
            //Debug.Log("GC 96: pos " + pos.z);
            for(int j = 0;  j < noObstacles;  j++) SpawnObstacles(ground.transform.position);
            Debug.Log("spawn obstacle");
            for(int j = 0;  j < noCadeaux;  j++) SpawnCadeaux(ground.transform.position);
            for(int j = 0;  j < noBonus;  j++) SpawnBonus(ground.transform.position);          
            groundsOnStage[i] = ground;
            pos.z += 60f;
        }
    }
    void SpawnObstacles(Vector3 pos)
    {
        int randIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = Instantiate(obstaclePrefabs[randIndex]);
        obstacle.transform.position = randPos(pos);   
    }
    void SpawnBonus(Vector3 pos)
    {
        GameObject bonus = Instantiate(bonusPrefabs[0]);
    }
    void SpawnCadeaux(Vector3 pos)
    {
        GameObject cadeau = Instantiate(cadeauPrefabs[0]);
        cadeau.transform.position = randPos(pos);
    }
    public Vector3 randPos(Vector3 pos)
    { // le ground mesure 20 en x et 60 en z
        float randX = UnityEngine.Random.Range(-8, 8);
        float randY = UnityEngine.Random.Range(0, 4);
        float randZ = UnityEngine.Random.Range(-25, 25);
        return new Vector3(pos.x + randX, pos.y + randY, pos.z + randZ);
    }

    void InitParamLevel() // initialize les paramètres du niveau et localize le Ship
    {
        levelUser = DataManager.levelUser;
        GameOver = false;
 //************* init paramètres de niveau ****************************************************
        toCollect = 5 + (Level + levelUser - 1) * 5; // par niveau
        noCadeaux = 2 + Level; // spawn pour chaque ground
        Debug.Log("no cadeaux : " + noCadeaux);
        noObstacles = Level ; // pour chaque ground
        Debug.Log("no obstacles : " + noObstacles);
        noBonus = 1 + (int)(Level + 3) / 5;
        Debug.Log("no bonus : " + noBonus);
        if (noBonus < 0) noBonus = 1;   // pour chaque ground
        collected = 0;
        health = 100;
        Distance = 0;
        lastDistance = 0;
        combo = 1;
        currentCombo = combo;
        comboTimer = 0f;
        Score = 0;
        //GOReason = " ";
        Ship = GameObject.Find("ShipObject");
        //if (Ship != null)
        Ship.transform.position = new Vector3(0, 0.4f, 0);
        Ship.GetComponentInChildren<ScriptFrozen>().chargesFrozen = 3;
        //Debug.Log("ship found in game controler");
        posZ = Ship.transform.position.z;
        currentSpeed = Ship.GetComponent<ShipController>().currentSpeed;
        bonusGC = new BonusClass();
        bonusGC.bonusType = BonusClass.BonusType.noBonus;
        bonusState.text = bonusGC.state.ToString();
        bonusType.text = bonusGC.bonusType.ToString();

    }// Update is called once per frame
    void Update()
    {
        if (state == GameState.Play)
        {
            UpdateTerrain();
            UpdateHealth();
            UpdateBonus();
            UpdateScore();
        }
        else if (state == GameState.Start)
        {
            Level = 1;
            if (Ship != null)
            {
                InitParamLevel();
                InitTerrain();
                state = GameState.Play;
            }
            else Debug.Log("Ship is null");
        }
        else if (state == GameState.NewLevel)
        {
            if (Ship != null)
            {
                InitTerrain();
                InitParamLevel();
                state = GameState.Play;
            }
        }
        else if (state == GameState.CheckPoint)
        {
            state = GameState.Wait;

            Debug.Log("state Check Point reached at " + DistanceRun + " m ; ");
            CheckPoint();
            ScoreFinal += Score;
            if (collected >= toCollect)
            {
                Debug.Log("Check Point : enough presents collected");
                Level++;
                state = GameState.NewLevel;
            }
            else
            {
                Debug.Log("Check Point : Game Over car pas assez de cadeaux collectés");
                GOReason.text = "Not enough presents collected";
                state = GameState.GameOver;
            }
        }
        else if (state == GameState.GameOver)
        {
            state = GameState.Wait;
            GameOverMethod("GAME OVER ");  //+ GOReason.text);
            Debug.Log("Called GameOver Method");
        }
        else if (state == GameState.Pause)
        {
            Debug.Log("game paused 5 sec");
            //StartCoroutine(Wait5Seconds());
        }
    }

    public void UpdateBonus()
    {
        //bonusGC.type = bonusCollected.name;
        
        if (bonusGC.state == BonusClass.BonusStatus.active)
        {
            bonusGC.timer += Time.deltaTime;
            if (bonusGC.timer < 4)
            {
                bonusGC.timer += Time.deltaTime;
                Debug.Log("timer du bonus : " + bonusGC.timer);
            }
            // update le timer du bonus activé
            else
            {
                bonusGC.timer = 0; // desactivation du bonus
                bonusGC.state = BonusClass.BonusStatus.noBonus;
                bonusGC.bonusType = BonusClass.BonusType.noBonus;
                Debug.Log(" timer fini");
            }
        }
        bonusState.text = "BONUS " + "\n" + bonusGC.state.ToString() + "\n " + System.Math.Round(GameControler.Instance.bonusGC.timer, 1);
        bonusType.text = bonusGC.bonusType.ToString();

    }
    void UpdateScore()
    {
        if (Ship != null) Distance = (int)(Ship.transform.position.z); // % DistanceRun;
        var = (int)((Distance - lastDistance) * combo);
        if (var > 0)
        {
            Score += var;
            lastDistance = Distance;

        }
    }

    void UpdateHealth()
    {
        Debug.Log("UpdateHealth");
        //score, dist, health, combo
        float coef = (float)health / 100;
        //Debug.Log("barre verte " + HealthGreen.rectTransform.sizeDelta.x);
        //Debug.Log("barre rouge " + HealthRed.rectTransform.sizeDelta.x);
        //***************** update de la barre de vie verte ****************************************************
        if ((HealthGreen.rectTransform != null) && (HealthRed.rectTransform != null))
        {
            HealthGreen.rectTransform.sizeDelta = new Vector2(HealthRed.rectTransform.sizeDelta.x * coef,
            HealthRed.rectTransform.sizeDelta.y);
        }
        if (health < 10)
        {
            Debug.Log("Warning! vehicule damaged");
        }
        if (health <= 0)
        {
            Debug.Log("Game Over");
            state = GameState.GameOver;
            GOReason.text = "Health 0";
            //GameOverMethod(" ", "HEALTH 0 ");
            //DataManager.Instance.SaveData();
        }
    }

    void UpdateTerrain()
    {
        Debug.Log("UpdateTerrain");
        //Debug.Log("GC 219: ground.pos.z " + pos.z);
        //UpdateHealth();
        if ((Ship != null) && (Ship.transform.position.z < DistanceRun))
        {
            for (int i = numberOfGrounds - 1; i >= 0; i--)
            {
                Debug.Log("in update terrain");
                GameObject ground = groundsOnStage[i];
                //Debug.Log("ground = ground on stage");
                if (ground.transform.position.z + 40 < Ship.transform.position.z - 6)
                {
                    //Debug.Log("if in updateTerrain");
                    float z = ground.transform.position.z;

                    int randomIndex = UnityEngine.Random.Range(0, groundsPrefabs.Length);//** choix d'un prefab ground *                                                                                    
                    GameObject newGround = Instantiate(groundsPrefabs[randomIndex]);// création de nouveau ground *****************
                    //GameObject newGround = Instantiate(groundsPrefabs[0]);
                    newGround.transform.position = new Vector3(0, 0f, z + 60 * numberOfGrounds);
                    for (int j = 0; j < noCadeaux; j++) { SpawnCadeaux(newGround.transform.position); Debug.Log("cadeau spawned"); }
                    for (int j = 0; j < noBonus; j++) { SpawnBonus(newGround.transform.position); Debug.Log("bonus spawned"); }
                    for (int j = 0; j < noObstacles; j++) { SpawnObstacles(newGround.transform.position); Debug.Log("cobstacle spawned"); }
                    Destroy(ground); //** destruction du ground testé ************************************************
                    groundsOnStage[i] = newGround;
                }
            }
        }
        else if (Ship.transform.position.z >= DistanceRun)
        { state = GameState.CheckPoint; }
    }
    
    IEnumerator DelayedLoadGOScene()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("2 sec sont passées, going to GameOverScene");
        SceneManager.LoadScene("GameOverScene");
    }
    void GameOverMethod(string Reason)
    {
        Debug.Log("GameOver; Score final: " + Score);
        //ScoreFinal += Score;
        if (Ship != null) Ship.GetComponent<ShipController>().moveSpeed = 0;

        textGO.text = "GAME OVER" + "\n" + Reason;
        scoreFinal.text = " SCORE FINAL : " + Score.ToString();
        DataManager.Instance.player.Score = Score;
        ArchiveScore();
        //message = RetrieveScore();
        StartCoroutine(DelayedLoadGOScene());
        state = GameState.Wait;
    }
    void ArchiveScore()
    {
        if (UpdateBestScore()) UpdateTop3();

        DataManager.Instance.SaveData();
    }
    bool UpdateBestScore()
    {
        bool newBestScore = false;
        if (DataManager.Instance.player.Score > DataManager.Instance.player.BestScore)
        {
            DataManager.Instance.player.BestScore = DataManager.Instance.player.Score;
            Debug.Log("New best score: " + DataManager.Instance.player.BestScore);
            newBestScore = true;
        }
        else
        {
            Debug.Log("Best score remains: " + DataManager.Instance.player.BestScore);
        }
        return newBestScore;
    }
    void UpdateTop3() // non utilisée
    {
        Debug.Log("Update Top3");
    }
    IEnumerator Wait3Seconds() // finalement non utilisée
    {
        Debug.Log("start coroutine Pause5");

        isPaused = true;
        yield return new WaitForSeconds(3f);
        Debug.Log("in coroutine 5");
        isPaused = false;
    }
   
    void Restart()
    {
        Debug.Log("restart game controler");
        InitTerrain();
        InitParamLevel();
    }
    void CheckPoint()
    {
        GameObject CheckP = Instantiate(CheckPointPrefab);
        CheckP.transform.position = new Vector3(0f, 0.4f, 600f);
        Ship.transform.position = new Vector3(0f, 0.4f, 560f);
        StartCoroutine(DelayedDestroyCheckP(CheckP));
    }

    IEnumerator DelayedDestroyCheckP(GameObject CheckP)
    {
        yield return new WaitForSeconds(4);
        Destroy(CheckP);
    }

    public class BonusClass
    {
        public float timer = 0f;
        public enum BonusType { magnetic, slow, health, invulnerable, noBonus, money } //6 types
        public enum BonusStatus { active, inactive, noBonus }
        public BonusType bonusType = BonusType.health;
        public BonusStatus state = BonusStatus.inactive;
    
        public void ActionHealth()
        {
            if (GameControler.Instance.health + 50 > 100) { GameControler.Instance.health = 100; }
            else GameControler.Instance.health += 50;
            Debug.Log("health added 50");
            state = BonusStatus.noBonus;
            bonusType = BonusType.noBonus;
        }

        public void ActionMagnetic()
        {
            GameObject[] magnetic; // se desactive tout de suite
            magnetic = GameObject.FindGameObjectsWithTag("cadeau");
            Debug.Log("collected magnétisés : " + magnetic.Length);
            GameControler.collected += magnetic.Length;
            state = BonusStatus.noBonus;
            bonusType = BonusType.noBonus;
        }

        public void ActionSlow()
        {
            timer += Time.deltaTime;
            state = BonusStatus.active;
            bonusType = BonusType.slow;           
         }

        public void ActionInvulnerable()
        {
            timer += 0; // va agir pendant 1.5 sec à partir de cet instant
            state = BonusStatus.active;
            bonusType = BonusType.invulnerable;
         }

        public void ActionMoney()
        {
            Score += 100; // se desactive tout de suite
            state = BonusStatus.noBonus;
            bonusType = BonusType.noBonus;

        }

        public void Activate()
        {
            timer = 0;
            if (bonusType == BonusType.health)
            {
                Debug.Log("health bonus activated");
                ActionHealth();
            }
            else if (bonusType == BonusType.slow)
            {
                Debug.Log("slow bonus activated");
                ActionSlow();
            }
            else if (bonusType == BonusType.magnetic)
            {
                Debug.Log("magnetic bonus activated");
                ActionMagnetic();
            }
            else if (bonusType == BonusType.invulnerable)
            {
                Debug.Log("invulnerable bonus activated");
                ActionInvulnerable();
            }
            else
            {
                Debug.Log("No bonus collected");
            }
        }
    }
  
    public void ActivateBonus()
    {
        bonusGC.Activate();
    }
    

}

