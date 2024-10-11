using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public Text bestText;
    // Start is called before the first frame update
    void Start()
    {
        bestText.text = "Best Score : " + ScoreManager.bestScore;
    }
}
