using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private BoxCollider2D playerBc;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private AudioSource playerAudio;

    public AudioClip jumpSound;
    public AudioClip slideSound;
    public AudioClip crashSound;
    public AudioClip powerUpSound;

    public int jumpCount = 0;
    public float gravityModifier = 5;
    public float jumpForce = 1000;
    public bool isOnGround = true;
    public bool powerUp = false;
    public bool gameOver = false;
    public bool canJump = true;
    public bool canSilde = true;
    private bool invincible = false;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBc = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();
        //Physics2D.gravity *= gravityModifier;
        originalColliderSize = playerBc.size;
        originalColliderOffset = playerBc.offset;


        Physics2D.gravity = new Vector2(0, -50);
        gravityModifier = 5;
        jumpForce = 1000;
        transform.position = new Vector2(-42.5f, -1.64f);
    }

    // Update is called once per frame
    void Update()
    {
        if(powerUp)
        {
            StartCoroutine(PlayerPowerUpEnd(4));
        }
    }
    public void Jump()
    {
        //float velocityX = playerRb.velocity.x;
        if (isOnGround == true && jumpCount > 0 && canJump == true)
        {
            //playerRb.velocity = new Vector2(velocityX, 0);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnimator.SetTrigger("Jump");
            isOnGround = false;
            jumpCount--;
            playerAudio.PlayOneShot(jumpSound,0.6f);
        }
    }
    public void OnPointDown()
    {
        if (isOnGround == true && canSilde == true)
        {
            playerBc.size = new Vector2(originalColliderSize.x, originalColliderSize.y - 0.6f);
            playerBc.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - 0.3f);
            playerAnimator.SetBool("Slide", true);
            isOnGround = false;
            canJump = false;
            playerAudio.PlayOneShot(slideSound,1.2f);
        }
    }
    public void OnPointUp()
    {
        playerBc.size = new Vector2(originalColliderSize.x, originalColliderSize.y);
        playerBc.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y);
        playerAnimator.SetBool("Slide", false);
        isOnGround = true;
        canJump = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && powerUp == false)
        {
            isOnGround = true;
            jumpCount++;
        }
        else if (invincible && (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Ostacle")))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Box") && powerUp == false)
        {
            gameOver = true;
            SceneManager.LoadScene("GameOver");
        }
        else if (collision.gameObject.CompareTag("Ostacle") && powerUp == false)
        {
            gameOver = true;
            SceneManager.LoadScene("GameOver");
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            PlayerPowerUp();
            playerAudio.PlayOneShot(powerUpSound, 0.6f);
            Destroy(collision.gameObject);
            ScoreManager.score += 5;
        }
        if (collision.gameObject.CompareTag("Box") && powerUp == true)
        {
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(crashSound,1.7f);
            ScoreManager.score += 2;
        }
        else if (collision.gameObject.CompareTag("Ostacle") && powerUp == true)
        {
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(crashSound,1.7f);
            ScoreManager.score += 3;
        }
    }
    private void PlayerPowerUp()
    {
        if (powerUp == false)
        {
            jumpCount = 0;
            powerUp = true;
            canSilde = false;

            Transform playerTransform = GetComponent<Transform>();
            playerTransform.localScale *= 1.8f;

            MoveLeft backGroundSpeed = GameObject.Find("BackGround").GetComponent<MoveLeft>();
            MoveLeft groundSpeed = GameObject.Find("Ground").GetComponent<MoveLeft>();
            if (backGroundSpeed != null)
            {
                backGroundSpeed.SetMoveSpeed(backGroundSpeed.moveSpeed += 20);
            }
            if (groundSpeed != null)
            {
                groundSpeed.SetMoveSpeed(groundSpeed.moveSpeed += 20);
            }
        }
    }
    private IEnumerator PlayerPowerUpEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (powerUp == false)
        {
            yield break;
        }
        jumpCount = 0;
        powerUp = false;
        canSilde = true;

        Transform playerTransform = GetComponent<Transform>();
        playerTransform.localScale /= 1.8f;

        MoveLeft backGroundSpeed = GameObject.Find("BackGround").GetComponent<MoveLeft>();
        MoveLeft groundSpeed = GameObject.Find("Ground").GetComponent<MoveLeft>();
        if (backGroundSpeed != null)
        {
            backGroundSpeed.SetMoveSpeed(backGroundSpeed.moveSpeed -= 20);
        }
        if (groundSpeed != null)
        {
            groundSpeed.SetMoveSpeed(groundSpeed.moveSpeed -= 20);
        }
        StartCoroutine(AfterPowerUp(0.8f));
    }
    private IEnumerator AfterPowerUp(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }
}

