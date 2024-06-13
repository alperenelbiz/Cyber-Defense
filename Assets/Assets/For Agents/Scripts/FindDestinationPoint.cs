using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FindDestinationPoint : MonoBehaviour
{
    public Transform destinationPoint;
    public EnemyData enemyData;
    public Slider healthBarPrefab;

    NavMeshAgent agent;
    int currentHealth;
    Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destinationPoint.position);
        agent.speed = enemyData.speed;
        currentHealth = enemyData.maxHealth;
        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.maxValue = enemyData.maxHealth;
        healthBar.value = enemyData.maxHealth;
    }

    public void Hit(int power)
    {
        if (healthBar)
        {

            healthBar.value -= power;

            if (healthBar.value <= 0)
            {
                Destroy(healthBar.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f && agent.hasPath)
        {
            LevelManager.RemoveEnemy();
            agent.ResetPath();
            Destroy(healthBar.gameObject);
            Destroy(this.gameObject, 0.1f);
        }
        if (healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 1.2f);
        }
    }
}