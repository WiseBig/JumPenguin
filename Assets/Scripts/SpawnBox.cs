using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public GameObject boxPrefabs;
    public GameObject otaclePrefabs;
    public GameObject powerUpPrefabs;

    //private PlayerController playerControllerScript;
    private int maxCount = 5;
    private List<GameObject> gameObjectsList = new List<GameObject>();
    private float minDistanceBetweenBoxes = 15;
    private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject playerObject = GameObject.Find("Player");
        //playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnInstance", 0, 2);
        InvokeRepeating("SpawnPowerUp", Random.Range(10, 15), Random.Range(15, 31));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            if(gameObject.CompareTag("Box") || gameObject.CompareTag("Otacle"))
            {
                gameObjectsList.Remove(gameObject);
                SpawnSingleBox();
                SpawnSingleOtacle();
            }
        }    
    }
    void SpawnInstance()
    {
        
            float randomValue = Random.value;

            if (randomValue < 0.5f)
            {
                SpawnSingleBox();
            }
            else
            {
                SpawnSingleOtacle();
            }
            if (gameObjectsList.Count > maxCount)
            {
                DestroyOldestObjects();
            }
  
    }
    void SpawnSingleOtacle() //장애물 생성 1
    {
        float randomX = Random.Range(-20, 10);
        Vector2 spawnPos = new Vector2(randomX, 0);

        while (IsTooClseExitstingBoxes(spawnPos))
        {
            randomX = Random.Range(-20, 10);
            spawnPos = new Vector2(randomX, 0);
        }

        GameObject newOtacle = Instantiate(otaclePrefabs, spawnPos, Quaternion.identity);
        gameObjectsList.Add(newOtacle);
    }
    void SpawnSingleBox() //장애물 생성 2
    {
        float randomX = Random.Range(-20, 10);
        Vector2 spawnPos = new Vector2(randomX, -3.5f);

        while (IsTooClseExitstingBoxes(spawnPos))
        {
            randomX = Random.Range(-20, 10);
            spawnPos = new Vector2(randomX, -3.5f);
        }

        GameObject newBox = Instantiate(boxPrefabs, spawnPos, Quaternion.identity);
        gameObjectsList.Add(newBox);
    }
    void SpawnPowerUp()
    {
        Vector2 spawnPos = new Vector2(-42.96f, 8.89f);

        GameObject newPowerUp = Instantiate(powerUpPrefabs, spawnPos, Quaternion.identity);
    }
    bool IsTooClseExitstingBoxes(Vector2 position) //장애물끼리의 거리 조절
    {
        foreach(GameObject existingbox in gameObjectsList)
        {
            if(existingbox != null && existingbox.activeSelf)
            {
                float distance = Vector2.Distance(existingbox.transform.position, position);

                if (distance < minDistanceBetweenBoxes)
                {
                    return true;
                }
            }
        }
        return false;
    }
    void DestroyOldestObjects()
    {
        if (gameObjectsList.Count > 0)
        {
            GameObject oldestBox = gameObjectsList[0];
            gameObjectsList.RemoveAt(0);
            Destroy(oldestBox);
        }
    }
}
