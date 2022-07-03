using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
   private Transform playerTransform;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
  

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = playerTransform.position;
    }
}
