using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindDestinationPoint : MonoBehaviour
{
    public Transform destinationPoint;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destinationPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f && agent.hasPath)
        {
            LevelManager.RemoveEnemy();
            agent.ResetPath();
            Destroy(this.gameObject, 0.1f);
        }
    }
}