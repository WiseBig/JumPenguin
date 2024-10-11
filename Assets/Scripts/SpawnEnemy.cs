using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public GameObject EnemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnEnemyInstance", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemyInstance()
    {
        float RandomX = Random.Range(-15, 7);
        Vector2 spawnPos = new Vector2(RandomX, 2.5f);
            Instantiate(EnemyPrefabs, spawnPos, Quaternion.identity);
    }
}
