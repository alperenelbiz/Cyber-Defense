using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgent : MonoBehaviour
{
    public GameObject agentPrefab;
    public Transform destinationPointLocation;
    public float startDelay = 1f;
    public float spawnRate = 0.3f;
    public int maxCount = 10;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawner", startDelay, spawnRate);
    }

    //Spawner is called to instantiate agents on a specific location
    void Spawner()
    {
        GameObject agent = Instantiate(agentPrefab, transform.position, Quaternion.identity);
        agent.GetComponent<FindDestinationPoint>().destinationPoint = destinationPointLocation;
        count++;

        if (count >= maxCount)
            CancelInvoke("Spawner");
    }
}
