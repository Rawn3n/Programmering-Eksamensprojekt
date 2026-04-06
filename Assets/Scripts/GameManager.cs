using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    bool isGameOver = false;
    //private MapChooser mapChooser;

    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private GameObject[] players;
    [SerializeField] public List<GameObject> playerList = new List<GameObject>();
    [SerializeField] private UI_Shoot[] playerHUDs;

    private float p1Score = 0;
    private float p2Score = 0;


    void Awake()
    {

        //mapChooser = GetComponent<MapChooser>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitLevel(Transform[] spawnpoints, UI_Shoot[] playerHUDs)
    {
        playerList.Clear();
        this.spawnpoints = spawnpoints;
        this.playerHUDs = playerHUDs;
        SpawnPlayers();
    }


    void SpawnPlayers()
    {
        List<Transform> playerSpawnpoints = new List<Transform>(spawnpoints);
        for (int i = 0; i < players.Length; i++)
        {
            if (playerSpawnpoints.Count == 0)
            {
                Debug.LogWarning("Ikke nok spawnpoints til alle spillere!");
                return;
            }

            // Vælg tilfældigt spawnpoint
            int index = Random.Range(0, playerSpawnpoints.Count);
            Transform spawn = playerSpawnpoints[index];
            playerSpawnpoints.RemoveAt(index);
            Debug.Log($"Spawning player {i} at {spawn.position}");

            // Instantiate player
            GameObject player = Instantiate(players[i], spawn.position, spawn.rotation);
            Debug.Log("player spawned");
            playerList.Add(player);


            // Find TankShooting på spilleren
            TankShooting tank = player.GetComponent<TankShooting>();

            // Find HUD’en for denne spiller i Canvas
            UI_Shoot hud = playerHUDs[i]; // antag du har en array af HUDs i samme rækkefølge som players

            if (i == 0)
            {
                playerHUDs[i].scoreText.text = $"{p1Score}";
            }
            else if (i == 1)
            {
                playerHUDs[i].scoreText.text = $"{p2Score}";
            }
            //playerHUDs[i].scoreText.text = $"Player {i + 1} Score: {(i == 0 ? p1Score : p2Score)}";


            // Tilknyt HUD til spilleren
            hud.SetTank(tank);
            

        }
    }
    public void PlayerDied()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            //mapChooser.RandomLevel();
            StartCoroutine(RestartGame());
        }
        else if (isGameOver)
        {
            if (playerList.Count <= 0)
            {
                //Score(null);
            }
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);
        //mapChooser.RandomLevel();
        LoadRandomLevel();

        if (playerList.Count <= 0)
        {
            Score(null);
        }
        else
        {
            Score(playerList[0]);
        }

        isGameOver = false;
        //playerList.Clear();
        //InitLevel(spawnpoints, playerHUDs);
    }


    private void Score(GameObject winner)
    {
        if (winner == null)
        {
            Debug.LogWarning("Ended in a draw");
            return;
        }

        //Debug.Log($"{winner.name} wins!");

        //if (winner.CompareTag("Player1"))
        //{
        //    p1Score++;
        //}
        //else if (winner.CompareTag("Player2"))
        //{
        //    p2Score++;
        //}
        if (winner.name == "Player1(Clone)")
        {
            p1Score++;
            Debug.Log($"Player 1 wins! Score: {p1Score}");
            //p1ScoreText.text = $"{p1Score}";
        }
        else if (winner.name == "Player2(Clone)")
        {
            p2Score++;
            Debug.Log($"Player 2 wins! Score: {p2Score}");
            //p2ScoreText.text = $"{p2Score}";

        }
    }

    void LoadRandomLevel()
    {
        Debug.Log("Loading new map");
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(Random.Range(1, sceneCount));
    }
}


