using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;



public class DataManager : MonoBehaviour
{

    public static DataManager Instance;
    public PlayerData oPlayerData;
    public Player player;
    public Player oPlayer;
    public static int levelUser = 1; //easy par défaut
    //public static ScriptInputName scriptInputName;

    public TMP_InputField inputName;
    public static string playerName;
    public Player[] players;
    public static string bestPlayerText = "";
    public static string YourBestScore = "";
    public string path;


    [System.Serializable]
    public class Player
    {
        public string Name = "Name";
        public int Score = 0;
        public int BestScore = 0;
    }
    /*public class PlayerList
    {
        public Player[] players;
    }
    public class Top3
    {
        public Player[] Top3Players;
    }*/

    [System.Serializable]
    public class PlayerData
    {
        public Player[] listOfPlayers;
        public Player BestPlayer;
        //public Player[] Top3;
    }

    void Awake()
    {
        Debug.Log("Awake Data Manager");
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Debug.Log("player name is " + playerName);
        Debug.Log("path : " + Application.persistentDataPath);
        //DontDestroyOnLoad(gameObject);

    } // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //playerName = "";
        Debug.Log("Start Data Manager");
        player = new Player();
        player.Score = 0;
        player.BestScore = 0;
        player.Name = playerName;
        Debug.Log("Start DataManager name set to " + player.Name);
        path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("path set to " + path);
    }
    public void SetName()
    {
        playerName = inputName.text;
        //if (playerName == "") { player.Name = "Unknown"; return; }
        //player.Name = playerName;
        Debug.Log("input name " + playerName);
    }

//test if player connu et update son Best Score et son last Score
    public bool NewPlayer(Player player, Player[] players)
    {
        bool isNew = true;
        foreach (Player known in players)
        {
            if (known.Name == player.Name)
            {
                isNew = false;
                oPlayer = known; // recupère les données du player connu; vérifier si besoin de garder
                player.BestScore = known.BestScore;
                known.Score = player.Score; // Score garde le dernier Score du joueur
                if (known.BestScore < player.Score)
                {
                    known.BestScore = player.Score;
                }
            }
        }
        return isNew;

    }

    PlayerData LoadData()
    {
        path = Application.persistentDataPath + "/savefile.json";
        //File.Delete(path); //pour reinitialiser le fichier ;
        player.Name = playerName;
        //if (playerName == "") player.Name = "Unknown";
        Debug.Log("player name is " + player.Name);

        //PlayerData dataLoaded = new PlayerData();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            oPlayerData = JsonUtility.FromJson<PlayerData>(json);

            //PlayerList listOfPlayers = dataLoaded.listOfPlayers;
            //Top3 top3= data.top3Players;
            Debug.Log("LoadData-154 Data loaded from " + path);
            Debug.Log("155 Data: " + oPlayerData.listOfPlayers);
        }
        return oPlayerData;
    }
    public void SaveData()
    {
        if (playerName == "") playerName = "Unknown";
        Debug.Log("165 Name " + playerName);
        oPlayerData = LoadData();

        if (oPlayerData.listOfPlayers.Length == 0)
        {
            oPlayerData = new PlayerData();
            oPlayerData.listOfPlayers = new Player[1];
            oPlayerData.listOfPlayers[0] = player;
            oPlayerData.BestPlayer = player;
            //players = oPlayerData.listOfPlayers;
            Debug.Log("created new player data for " + player.Name);
        }
        else if (NewPlayer(player, oPlayerData.listOfPlayers))
        {
            oPlayerData.listOfPlayers = oPlayerData.listOfPlayers.Append(player).ToArray();
            Debug.Log("Added new player " + player.Name);
            Debug.Log("list " + oPlayerData.listOfPlayers);
        }
        else if (!NewPlayer(player, oPlayerData.listOfPlayers))
        {
            Debug.Log("Known player, best score loaded " + oPlayer.BestScore);
        }
        players = oPlayerData.listOfPlayers;
        Debug.Log("DM-185 players " + players.Length); /// check list of players
        Debug.Log("DM-186 players " + players);
        /*init list of players and top3
        //PlayerList listOfPlayers = new PlayerList();
        //playersNamesList = oPlayerData.listOfPlayersNames;

        //Top3 top3Players = new Top3();
        top3Players.Top3Players = new Player[] { player, player, player };

        /*else if ((player.Name != "Unknown") && (NewPlayer(player, players)))
        {
            Debug.Log("164 players " + oPlayerData.listOfPlayers.Length);
            oPlayerData.listOfPlayers = oPlayerData.listOfPlayers.Append(player).ToArray();
            Debug.Log("Added new player " + player.Name);
            Debug.Log("167 players " + oPlayerData.listOfPlayers.Length); // check list of players

        }
        else if (!NewPlayer(player, players))
        {
            Debug.Log("Known player, ancien best score loaded " + player.BestScore);
        }
        string path = Application.persistentDataPath + "/savefile.json";
        if (NewPlayer())
        { }// Add new player to the list
        else
        if (HighScore())
        { }// Update player's best score and check + update Top3

        PlayerData dataToSave = new PlayerData
        {
            listOfPlayers = data.listOfPlayers,
            //top3Players = top3Players,
        };*/
        UpdatePlayerData();
        //Debug.Log(" data to save " + oPlayerData);
        string json = JsonUtility.ToJson(oPlayerData);
        File.WriteAllText(path, json);
        Debug.Log("182 Data saved json" + json);
        Debug.Log("Best Player Score : " + oPlayerData.BestPlayer.BestScore);
        Debug.Log("Best Player: " + oPlayerData.BestPlayer.Name + " with " + oPlayerData.BestPlayer.Score + " points");
        bestPlayerText = "Best Player: " + oPlayerData.BestPlayer.Name + " with " + oPlayerData.BestPlayer.BestScore + " points";
        YourBestScore = "Your Best Score: " + player.BestScore + " points";
    }
    public void UpdatePlayerData()
    {
        Debug.Log("update PlayerData before SaveData");       
            if (oPlayerData.BestPlayer.BestScore < player.BestScore)
            {
                oPlayerData.BestPlayer = player;
                Debug.Log("Best player updated to you : " + oPlayerData.BestPlayer.Name +
                " Best score is " + oPlayerData.BestPlayer.Score);
            }
        
        //oPlayerData.listOfPlayers = players;
    }

    /*bool HighScore()
    {
        bool isHighScore = false;
        if (p.PlayerName == playerName && p.PlayerScore > playerBestScore)
            foreach (Player p in Top3.Top3Players)
            {
                if (p.PlayerName == playerName && p.PlayerScore > playerBestScore)
                {
                    isHighScore = true;
                    p.PlayerBestScore = p.PlayerScore; // New personal best score
                }
            }
        return isHighScore; // No new high score
    }*/

    public void Restart()
    {
        Debug.Log("goto Game from DataManager");
        SceneManager.LoadScene("GameScene");        
    }
}

