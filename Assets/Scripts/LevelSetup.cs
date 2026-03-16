using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private UI_Shoot[] playerHUDs;

    private void Awake()
    {
        GameManager.Instance.InitLevel(spawnpoints, playerHUDs);
    }
}