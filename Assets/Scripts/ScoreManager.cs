using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static int bestScore = 0;
    public static bool newHighScore;
    public GameObject operation;

    public Text scoreText;
    private CurrentScore currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = GetComponent<CurrentScore>();
        newHighScore = false;
        score = 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        StartCoroutine(OperationOff());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        BestScore();
    }
    void BestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // ���� ������ ��� ��ũ�� ����

            newHighScore = true;
        }
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save();
    }

    IEnumerator OperationOff()
    {
        yield return new WaitForSeconds(2f);
        operation.gameObject.SetActive(false);
    }
}
