using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReset : MonoBehaviour
{
    public void ResetBestScore()
    {
        PlayerPrefs.DeleteKey("BestScore");
        ScoreManager.bestScore = 0; // �޸𸮿����� �ʱ�ȭ
        PlayerPrefs.Save(); // ���� ������ ��ũ�� ����
    }
}
