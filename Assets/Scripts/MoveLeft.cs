using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float leftBound = -60;
    private PlayerController playerControllerScript;
    public float moveSpeed = 5;
    public float currentMoveSpeed = 5;
    public float accelerationRate = 0.2f;
    // Start is called before the first frame update 
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerControllerScript = playerObject.GetComponent<PlayerController>();
        }
        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            currentMoveSpeed = moveSpeed + Time.timeSinceLevelLoad * accelerationRate;

            transform.Translate(Vector2.left * currentMoveSpeed * Time.deltaTime);

            if (transform.position.x < leftBound && gameObject.CompareTag("Ostacle"))
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }
}
