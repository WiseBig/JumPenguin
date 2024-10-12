using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataReset : MonoBehaviour
{
    public GameObject gameDataResetText;
    public void ResetBestScore()
    {
        gameDataResetText.gameObject.SetActive(true);
        StartCoroutine(ShowText());
        PlayerPrefs.DeleteKey("BestScore");
        ScoreManager.bestScore = 0; 
        PlayerPrefs.Save(); 
    }
    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(1);
        gameDataResetText.gameObject.SetActive(false);
    }
}
