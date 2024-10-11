using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOstacle : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public GameObject ostaclePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnOstacleInstance", 1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnOstacleInstance()
    {
        float RandomX = Random.Range(-10, 65);
        Vector2 spawnPos = new Vector2(RandomX, 2.5f);
        Instantiate(ostaclePrefabs, spawnPos, Quaternion.identity);
    }
}
