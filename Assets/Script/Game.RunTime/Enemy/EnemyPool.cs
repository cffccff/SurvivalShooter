using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject HellephantPrefabs;
    public GameObject ZomBearPrefabs;
    public GameObject ZomBunnyPrefabs;
    private float timeSpawn;
    private float timeSpawnThreshold;
    private List<GameObject> pooledHellephant = new List<GameObject>();
    private List<GameObject> pooledZomBear = new List<GameObject>();
    private List<GameObject> pooledZomBunny = new List<GameObject>();
    private int amountToPool = 10;
    public static EnemyPool Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        //for (int i = 0; i < amountToPool; i++)
        //{
        //    GameObject obj = Instantiate(HellephantPrefabs);
        //    obj.SetActive(false);
        //    pooledHellephant.Add(obj);
        //}
        AddEnemyPrefabToList(HellephantPrefabs, pooledHellephant);
        AddEnemyPrefabToList(ZomBearPrefabs, pooledZomBear);
        AddEnemyPrefabToList(ZomBunnyPrefabs, pooledZomBunny);
    }
    private void AddEnemyPrefabToList(GameObject enemyPrefab, List<GameObject> list)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(false);
            list.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledHellephant.Count; i++)
        {
            if (!pooledHellephant[i].activeInHierarchy)
            {
                return pooledHellephant[i];
            }
        }
        return null;
    }
    public GameObject GetPooledHellephant()
    {
        for (int i = 0; i < pooledHellephant.Count; i++)
        {
            if (!pooledHellephant[i].activeInHierarchy)
            {
                return pooledHellephant[i];
            }
        }
        return null;
    }
    public GameObject GetPooledZomBear()
    {
        for (int i = 0; i < pooledZomBear.Count; i++)
        {
            if (!pooledZomBear[i].activeInHierarchy)
            {
                return pooledZomBear[i];
            }
        }
        return null;
    }
    public GameObject GetPooledZomBunny()
    {
        for (int i = 0; i < pooledZomBunny.Count; i++)
        {
            if (!pooledZomBunny[i].activeInHierarchy)
            {
                return pooledZomBunny[i];
            }
        }
        return null;
    }
    private void Update()
    {
        
    }
}
