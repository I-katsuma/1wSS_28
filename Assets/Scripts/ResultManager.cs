using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    [SerializeField] private Text stage1time;
    [SerializeField] private Text stage2time;
    [SerializeField] private Text stage3time;

    [SerializeField] private Text TotalTime;

    [SerializeField] private Text RankText;

    private int[] clearM;
    private float[] clearS;


    private void Day1LapApper()
    {
        clearM[0] = PlayerPrefs.GetInt("BEST_MINUTE_01", 0);
        clearS[0] = PlayerPrefs.GetFloat("BEST_SECONDES_01", 0);

        stage1time.text = "DAY1  " + clearM[0].ToString("00") + ":" + ((int)clearS[0]).ToString("00");
    }

    private void Day2LapApper()
    {
        clearM[1] = PlayerPrefs.GetInt("BEST_MINUTE_02", 0);
        clearS[1] = PlayerPrefs.GetFloat("BEST_SECONDES_02", 0);

        stage1time.text = "DAY2  " + clearM[1].ToString("00") + ":" + ((int)clearS[1]).ToString("00");
    }

    private void Day3LapApper()
    {
        clearM[2] = PlayerPrefs.GetInt("BEST_MINUTE_03", 0);
        clearS[2] = PlayerPrefs.GetFloat("BEST_SECONDES_03", 0);

        stage1time.text = "DAY3  " + clearM[2].ToString("00") + ":" + ((int)clearS[2]).ToString("00");
    }

    private void TotalLapApper()
    {
        int totalM = 0;
        float totalS = 0;

        for (int i = 0; i < clearM.Length; i++)
        {
            totalM += clearM[i];
            totalS += clearS[i];            
        }

        TotalTime.text = "TOTAL  " + totalM.ToString("00") + ":" + ((int)totalS).ToString("00");
    }

    void Start()
    {
        Day1LapApper();   
    }

    
}
