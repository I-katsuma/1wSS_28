using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text stage1time;

    [SerializeField]
    private Text stage2time;

    [SerializeField]
    private Text stage3time;

    [SerializeField]
    private Text TotalTime;

    [SerializeField]
    private Text RankText;

    [SerializeField] GameObject LeaderBoardPanel;


    private int[] clearM;
    private float[] clearS;

    public int RankScore = 0;
    public int minTvalue = 0;
    public int secTvalue = 0;
    private void initialize()
    {
        RankScore = 0;
        clearM = new int[] { 0, 0, 0 };
        clearS = new float[] { 0, 0, 0 };
    }

    private void Day1LapApper()
    {
        clearM[0] = PlayerPrefs.GetInt("BEST_MINUTE_01", 0);
        clearS[0] = PlayerPrefs.GetFloat("BEST_SECONDES_01", 0);

        stage1time.text =
            "DAY1  " + clearM[0].ToString("00") + ":" + ((int)clearS[0]).ToString("00");
    }

    private void Day2LapApper()
    {
        clearM[1] = PlayerPrefs.GetInt("BEST_MINUTE_02", 0);
        clearS[1] = PlayerPrefs.GetFloat("BEST_SECONDES_02", 0);

        stage2time.text =
            "DAY2  " + clearM[1].ToString("00") + ":" + ((int)clearS[1]).ToString("00");
    }

    private void Day3LapApper()
    {
        clearM[2] = PlayerPrefs.GetInt("BEST_MINUTE_03", 0);
        clearS[2] = PlayerPrefs.GetFloat("BEST_SECONDES_03", 0);

        stage3time.text =
            "DAY3  " + clearM[2].ToString("00") + ":" + ((int)clearS[2]).ToString("00");
    }

    private void TotalLapApper()
    {
        int totalMin = 0;
        float totalSec = 0;
        for (int i = 0; i < clearM.Length; i++)
        {
            totalMin += clearM[i];
        }
        for (int i = 0; i < clearS.Length; i++)
        {
            totalSec += Mathf.Floor(clearS[i]);
        }

        Debug.Log("totalMin: " + totalMin);
        Debug.Log("totalSec: " + totalSec);

        // 秒の処理
        double num60Div = Mathf.Floor(totalSec) / 60.0;
        // 桁数調整
        double FLnum60Div = Mathf.Floor((float)num60Div * Mathf.Pow(10, 2)) / Mathf.Pow(10, 2);
        
        Debug.Log("num60Div: " + num60Div);
        Debug.Log("FLnum60Div: " + FLnum60Div);

        // 行程A(秒) 小数点部分のみ抽出
        float valueA = (float) FLnum60Div;
        valueA = valueA - Mathf.Floor(valueA);
        Debug.Log("valueA: " + valueA);
        // 桁数調整
        //double FLnumSec = Mathf.Floor(valueA * Mathf.Pow(10, 2)) / Mathf.Pow(10, 2);
        //Debug.Log("FLnumSec: " + FLnumSec
        
        // 60掛けて四捨五入 秒のトータル値
        var secTotalnum = Mathf.Round((float)valueA * 60.0f);
        Debug.Log("secTotalnum: " + secTotalnum);


        // 行程B(分) num60divを整数のみに
        var numB = Mathf.Floor((float) num60Div);
        Debug.Log("numb: " + numB);

        minTvalue = (int)numB + (int)totalMin; // 分の合計値
        secTvalue = (int)secTotalnum;

        TotalTime.text =
            "TOTAL  " + minTvalue.ToString("00") + ":" + secTvalue.ToString("00");

        RankCheck(minTvalue, secTvalue);
    }

    private void RankCheck(int minT, int secT)
    {
        int numA = minT * 100;
        RankScore = numA + secT;

        if(RankScore < 220 && RankScore > 50)
        {
            RankText.text = "S";
        }else if(RankScore >= 220 && RankScore < 270)
        {
            RankText.text = "A";
        }else if(RankScore >= 270)
        {
            RankText.text = "B";
        }
        else if(RankScore <= 50)
        {
            RankText.text = "ERROR";
            RankScore = 1000;
        }
    }

    public void LeaderBoardPanelSwitch(bool x)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.ButtonPush);
        LeaderBoardPanel.SetActive(x);

    }

    /*
    private void TotalLapApper()
    {
        int totalM = 0;
        float totalS = 0;

        for (int i = 0; i < clearM.Length; i++)
        {
            totalM += clearM[i];
            totalS += clearS[i];
        }
        Debug.Log("totalM:" + totalM);
        Debug.Log("totalS:" + (int)totalS);
        float Num = 0;
        float Snum = 0;
        if(totalS > 60)
        {
            Num = totalS / 60;
            Snum = totalS - 60f;
        }
        int minNum = ((int)Num + totalM) * 100;
        int tNum = minNum + (int)Snum;

        if(tNum < 200)
        {
            RankText.text = "S";
        }else if(tNum >= 200 && tNum < 260)
        {
            RankText.text = "A";
        }else if(tNum >= 260)
        {
            RankText.text = "B";
        }


        Debug.Log("Num:" + Num + " Snum:" + Snum);
        Debug.Log("tNum:" + tNum + " = " + minNum + " + " + (int)Snum);

    }
    */

    void Start()
    {
        initialize();
        Day1LapApper();
        Day2LapApper();
        Day3LapApper();
        TotalLapApper();
    }
}
