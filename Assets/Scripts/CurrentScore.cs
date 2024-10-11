using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CurrentScore : MonoBehaviour
{
    public GameObject textPanel;
    public Text currentScore;

    // Start is called before the first frame update
    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        currentScore.text = "Score : " + ScoreManager.score;
        if (ScoreManager.newHighScore == true && ScoreManager.bestScore != 0)
        {
            textPanel.gameObject.SetActive(true);
        }
        else
        {
            textPanel.gameObject.SetActive(false);
        }
    }
}
