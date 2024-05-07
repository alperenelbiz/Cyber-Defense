using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject[] spawnPoints;
    static int totalEnemies = 0;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawn");
        foreach (GameObject sp in spawnPoints)
        {
            totalEnemies += sp.GetComponent<SpawnAgent>().maxCount;
        }
        Debug.Log(totalEnemies);
    }

    public static void RemoveEnemy()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
            Debug.Log("Level Over");
    }
}
