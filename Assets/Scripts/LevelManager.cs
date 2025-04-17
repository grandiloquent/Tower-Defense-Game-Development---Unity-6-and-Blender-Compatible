using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject[] spawnPoints;
    static int totalEnemies = 0;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            totalEnemies+= spawnPoint.GetComponent<Spawn>().maxCount;
        }
    }
    public static void RemoveEnemy()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies are dead!");
            // Load next level or show victory screen
        }
    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
}