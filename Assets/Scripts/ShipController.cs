//using System.Numerics;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
//** controle les inputs pour bouger le perso et les collisions  *******************************

public class ShipController : MonoBehaviour
{
    //[SerializeField] private float translationSpeed = 4f;
    [SerializeField] public float moveSpeed = 20f;
    [SerializeField] public float currentSpeed = 4f;
    //private float deltaSpeed = 0.1f;
    public static GameControler GameControler;
    private const float MIN_X = -10f;
    private const float MAX_X = 10f;
    private const float MIN_Y = 0.4f;
    private const float MAX_Y = 5f;
    private float moveX;
    private float nextX;
    private float nextY;
    private float nextZ;
    private int Distance = 0;
    public float deltaSpeed = 0.05f;
    //private bool isPaused = false;
    private int cent = 1;
    public bool isFrozen = false;
    public enum GameState { Play, NotPlay, Paused, CheckPoint, GameOver, NewLevel, Start }
    private GameState state;
    private float jumpTimer = 0f;
    [SerializeField] AudioClip[] songs;
    //int i = 0;
    //float time = 0;
    AudioSource sourceAudio;
    public GameObject FrozenBox;
    public Vector3 positionHidden;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Debug.Log("awake in ShipContr");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionHidden = new Vector3(0, 0, -300);
        FrozenBox = Instantiate(FrozenBox);
        FrozenBox.transform.position = positionHidden;
        cent = GameControler.Level; // pour auto update speed
        transform.position = new Vector3(0, MIN_Y, 0); // Initialize ship position at origin
        currentSpeed = 2f * GameControler.Level + 2;       
        moveSpeed = 15f + 4f * (GameControler.Level - 1);
        Debug.Log("Level: " + GameControler.Level);
        Debug.Log("ShipCo moveSpeed set to: " + moveSpeed); // max par niveau
    }

    public void Reset()
    //recommence le jeu après changement de niveau
    // TODO : verifier si necessaire
    {
        transform.position = new Vector3(0, MIN_Y, 0); // Initialize ship position at origin
        currentSpeed = 0.8f * GameControler.Level;
        moveSpeed = 20f + 10f * (GameControler.Level - 1);
        Debug.Log("Level: " + GameControler.Level);
        Debug.Log("moveSpeed set to: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        if (state == GameState.Play)
        {
            UpdateSpeed();
            InputMoves();
            transform.position = new Vector3(nextX, nextY, nextZ);
            Distance = (int)transform.position.z;
            if ((jumpTimer > 0) && (jumpTimer < 0.5))
            {
                jumpTimer += Time.deltaTime;
            }
            else if (jumpTimer >= 0.5)
            {
                nextY = transform.position.y - 5;
                jumpTimer = 0f;
            }
            if (isFrozen)
            {
                FrozenBox.transform.position = new Vector3(transform.position.x + 0.49f, transform.position.y + 2.25f, transform.position.z - 2.5f);
            }
            if ((!isFrozen) && (FrozenBox != null))
            {
                FrozenBox.transform.position = positionHidden;
            }
        }
        else if ((state == GameState.Start) || (state == GameState.NewLevel))
        {
            Reset();
        }

        /*if (state == GameState.Paused)
        {
            return;   //pause dans le jeu;
            //lastSpeed = currentSpeed;
            //currentSpeed = 0;
        }
        else if (state == GameState.GameOver)
        {

        }
        else if (state == GameState.CheckPoint)
        {
            //transform.position = new Vector3(0f, 0f,(float)GameControler.DistanceRun + 30);
            //state = GameState.GameOver;
        }*/

        //isPaused = GameControler.isPaused;
        //if (isPaused) StartCoroutine(Pause5Seconds());
        /*if (bonusActif != null)
        {
            if (bonusActif == "invulnerable") { GameControler.Instance.health = 100; }
            else if (bonusActif == "health") { GameControler.Instance.health += 20; }
            else if (bonusActif == "speed") { deltaCurrentSpeed = 10; moveSpeed += 20; }
            else if (bonusActif == "magnetic") { MagnetiseCadeaux(); }

        }*/


        //float zMove = 0f; // Initialize zMove to 0
        /*float horizontalMovement = Input.GetAxis("Horizontal");
        transform.position += transform.right * horizontalMovement * 4 * Time.deltaTime;
        if (transform.position.x > MAX_X)
        {
            transform.position = new Vector3(MAX_X - 3, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < MIN_X)
        {
            transform.position = new Vector3(MIN_X + 3, transform.position.y, transform.position.z);
        }*/
        /*float verticalMovement = Input.GetAxis("Vertical");
        if (verticalMovement! > 0)
        {
            verticalMovement -= 0.1f; //  gravity effect
        }
        transform.position += transform.up * verticalMovement * 4 * Time.deltaTime;
        if (transform.position.y > MAX_Y)
        {
            transform.position = new Vector3(transform.position.x, MAX_Y, transform.position.z);
        }
        else if (transform.position.y < MIN_Y)
        {
            transform.position = new Vector3(transform.position.x, MIN_Y, transform.position.z);
        }*/
        // space key to accelerate
        /*if (Input.GetKey(KeyCode.Space))
        {
            // accelère si bar espace appuyée
            currentSpeed += 1f;
            if (currentSpeed > moveSpeed)
            {
                currentSpeed = moveSpeed;
            }
        }*/
        /*else
        {
            // ralentit doucement si pas d'appui sur bar espace
            currentSpeed -= 0.0001f;
            if (currentSpeed < 0.1f)
            {
                currentSpeed = 0.1f;
            }
        }*/

        // avance
        /*transform.position += transform.forward * currentSpeed * Time.deltaTime;
        Distance = (int)transform.position.z;
        currentSpeed += deltaSpeed * Time.deltaTime / 10;*/
        //if (Distance >= 1000) Debug.Log("Run terminée");
    }
    void InputMoves()
    {
        float move = 0; //Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftArrow)) move = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) move = 1;
        //moveX = transform.position.x + move * 4;
        nextX = transform.position.x + move * 4;

        if (nextX > MAX_X)
        {
            //transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
            nextX = MAX_X;
        }
        else if (transform.position.x < MIN_X)
        {
            //transform.position = new Vector3(MIN_X + 3, transform.position.y, transform.position.z);
            nextX = MIN_X;
        }
        /*else transform.position += transform.right * move * 4;
        MusicPlay(2);*/
        if (Input.GetKey(KeyCode.Space) && (jumpTimer == 0)) // O signifie qu'il n'y a pas de jump en cours
        {
            jumpTimer += Time.deltaTime;
            nextY = transform.position.y + 5;
            //transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            // Ship monte à y + 5 et il y reste pendant 0.5 secondes;
        }
    }

    void UpdateSpeed()
    {
        nextZ = (transform.position + transform.forward * currentSpeed * Time.deltaTime).z;
        //Distance = (int)transform.position.z;
        currentSpeed += deltaSpeed * Time.deltaTime;
    }

    void UpdateState()
    {
        if (GameControler.state == GameControler.GameState.Play)
        {
            state = GameState.Play;
        }
        else if (GameControler.state == GameControler.GameState.Pause)
        {
            state = GameState.Paused;
        }
        else if (GameControler.state == GameControler.GameState.GameOver)
        {
            state = GameState.GameOver;
        }
        else if (GameControler.state == GameControler.GameState.CheckPoint)
        {
            state = GameState.CheckPoint;
        }
        else if (GameControler.state == GameControler.GameState.NewLevel)
        {
            state = GameState.NewLevel;
        }
        else if (GameControler.state == GameControler.GameState.Start)
        {
            state = GameState.Start;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //****** vérifie si collision avec cadeau ou obstacle ou bonus; 
        if ((other.tag == "bonus") && (state == GameState.Play))
        {
            int rint = UnityEngine.Random.Range(0,5); // choix au hazard du type de bonus
            if (rint == 0) GameControler.Instance.bonusGC.bonusType = GameControler.BonusClass.BonusType.health;
            else if (rint == 1) GameControler.Instance.bonusGC.bonusType = GameControler.BonusClass.BonusType.slow;
            else if (rint == 2) GameControler.Instance.bonusGC.bonusType = GameControler.BonusClass.BonusType.money;
            else if (rint == 3) GameControler.Instance.bonusGC.bonusType = GameControler.BonusClass.BonusType.magnetic;
            else GameControler.Instance.bonusGC.bonusType = GameControler.BonusClass.BonusType.invulnerable;

            GameControler.Instance.corectMoves += 1;
            GameControler.Instance.comboTimer = 0;
            MusicPlay(0);
            Debug.Log("playing sound collect bonus");
        }
        else if ((other.tag == "obstacle") && (state == GameState.Play))
        {
            GameControler.Instance.health -= 10;
            Debug.Log("Collision " + other.name);
            GameControler.Instance.corectMoves = 0;
            GameControler.Instance.comboTimer = 0;
            GameControler.Instance.combo = 1;
            MusicPlay(1);
            Debug.Log("playing sound collision");
        }
        else if ((other.tag == "cadeau") && (state == GameState.Play))
        {
            Debug.Log("Cadeau collecté");
            GameControler.collected += 1;
            GameControler.Instance.corectMoves += 1;
            MusicPlay(0);
            Debug.Log("playing sound collect cadeau");

        }
        UpdateCombo();       
    }

    void UpdateCombo()
    {
        if (!GameControler.GameOver)
            Debug.Log("correct = " + GameControler.Instance.corectMoves);
        // *** TODO combo pas encore developpé; 
        if ((GameControler.Instance.combo < 5) && (GameControler.Instance.corectMoves >= 5) && (GameControler.Instance.comboTimer < 5f))
        {
            GameControler.Instance.combo += 1;

        }

        //limite le combo à 5
        else if ((GameControler.Instance.comboTimer >= 5f) && (GameControler.Instance.corectMoves < 5))
        {
            if (GameControler.Instance.combo > 1) GameControler.Instance.combo -= 1;
            GameControler.Instance.comboTimer = 0;
            //GameControler.Instance.health += 20;
            //GameControler.combo = 0;
            //GameControler.combo = 1;
        }
        if (GameControler.Instance.combo > GameControler.Instance.currentCombo)
        {// il y a eu modification positive du combo
            Debug.Log("combo = " + GameControler.Instance.combo);
            //GameControler.Score += GameControler.Instance.currentCombo * (GameControler.Distance - GameControler.lastDistance);
            GameControler.Instance.currentCombo = GameControler.Instance.combo;
            GameControler.Instance.comboTimer = 0f;
            //GameControler.lastDistance = GameControler.Distance;
            GameControler.Instance.corectMoves = 0;
        }

    }
           
    /*IEnumerator Pause5Seconds()
    {
        Debug.Log("start coroutine Pause5");

        isPaused = true;
        yield return new WaitForSeconds(5f);
        Debug.Log("in coroutine 5");
        isPaused = false;
    }*/
    public void MusicPlay(int piste)
    {

        sourceAudio = GetComponent<AudioSource>();
        if (sourceAudio != null)
        {
            sourceAudio.volume = 0.09f;
            if (piste < songs.Length) sourceAudio.clip = songs[piste];
            else sourceAudio.clip = songs[0];
            sourceAudio.Play();
            sourceAudio.loop = false;
        }

    }    
}


