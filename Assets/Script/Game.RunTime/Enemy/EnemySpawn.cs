using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
  
    private float timeSpawn;
    private float timeSpawnThreshold;
    private GameObject enemy;

    private void Start()
    {
      
        timeSpawnThreshold = 4f;
      
    }
    private void CheckEnemyType()
    {
        if (gameObject.name.StartsWith("Hellephant"))
        {
            enemy = EnemyPool.Instance.GetPooledHellephant();
        }
        else if (gameObject.name.StartsWith("ZomBear"))
        {
            enemy = EnemyPool.Instance.GetPooledZomBear();
        }
        else
        {
            enemy = EnemyPool.Instance.GetPooledZomBunny();
        }
    }
    private void Update()
    {
        if (timeSpawn < timeSpawnThreshold)
        {
            timeSpawn += Time.deltaTime;
        }
        else
        {
            // Time is over theshold, player takes damage
            //  Debug.Log("Do dmg on stay");
            SpawnEnemyDelay();
              // Reset timer
              timeSpawn = 0f;
        }
    }
    private void SpawnEnemyDelay()
    {

        CheckEnemyType();
        if (enemy != null)
        {
            enemy.transform.position = this.transform.position;
            enemy.SetActive(true);
            enemy.GetComponent<EnemyNavMesh>().enabled = true;
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            enemy.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
