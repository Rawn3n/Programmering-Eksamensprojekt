using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    bool isGameOver = false;

    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private GameObject[] players;
    [SerializeField] private UI_Shoot[] playerHUDs;

    void Awake()
    {
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

    private void Start()
    {


        SpawnPlayers();
        Debug.Log("game manager start");
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

            // Find TankShooting på spilleren
            TankShooting tank = player.GetComponent<TankShooting>();

            // Find HUD’en for denne spiller i Canvas
            UI_Shoot hud = playerHUDs[i]; // antag du har en array af HUDs i samme rækkefølge som players

            // Tilknyt HUD til spilleren
            hud.SetTank(tank);

        }
    }
}


