using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField]
    GameObject ErrorText;

    [SerializeField]
    GameObject LeaderBoardPanel;

    [SerializeField]
    PlayfabLogin playfabLogin;

    private int[] clearM;
    private float[] clearS;

    public int RankScore = 0;
    public int TimeScore = 0;
    public int minTvalue = 0;
    public int secTvalue = 0;

    [SerializeField] RectTransform rectTransform;

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

        //Debug.Log("totalMin: " + totalMin);
        //Debug.Log("totalSec: " + totalSec);

        // ????????????
        double num60Div = Mathf.Floor(totalSec) / 60.0;
        // ????????????
        double FLnum60Div = Mathf.Floor((float)num60Div * Mathf.Pow(10, 2)) / Mathf.Pow(10, 2);

        //Debug.Log("num60Div: " + num60Div);
        //Debug.Log("FLnum60Div: " + FLnum60Div);

        // ??????A(???) ???????????????????????????
        float valueA = (float)FLnum60Div;
        valueA = valueA - Mathf.Floor(valueA);
        //Debug.Log("valueA: " + valueA);
        // ????????????
        //double FLnumSec = Mathf.Floor(valueA * Mathf.Pow(10, 2)) / Mathf.Pow(10, 2);
        //Debug.Log("FLnumSec: " + FLnumSec

        // 60????????????????????? ?????????????????????
        var secTotalnum = Mathf.Round((float)valueA * 60.0f);
        //Debug.Log("secTotalnum: " + secTotalnum);


        // ??????B(???) num60div??????????????????
        var numB = Mathf.Floor((float)num60Div);
        //Debug.Log("numb: " + numB);

        minTvalue = (int)numB + (int)totalMin; // ???????????????
        secTvalue = (int)secTotalnum;

        TotalTime.text = "TOTAL  " + minTvalue.ToString("00") + ":" + secTvalue.ToString("00");

        //RankCheck2(minTvalue, secTvalue);
        RankCheck(minTvalue, secTvalue);
    }


    /*
    public void RankCheck2(int minT, int SecT) // RankScore
    {
        int score = 0;
        switch (minT)
        {
            case 0:
            score = 100;
            break;
            case 1:
            score = 50;
            break;
            case 2:
            score = 25;
            break;
            default:
            score = 0;
            break;
        }

        RankScore = score + minT + SecT;
        
        if (RankScore > 150)
        {
            RankText.text = "S";
        }
        else if (RankScore <= 150 && RankScore > 100)
        {
            RankText.text = "A";
        }
        else if (RankScore <= 100)
        {
            RankText.text = "B";
        }
    }
    */

    private void RankCheck(int minT, int secT) // TimeScore
    {
        int num60 = minT * 60;
        int timeTotal = num60 + secT;
        Debug.Log("TimeTotal: " + timeTotal);
        TimeScore = 500 - timeTotal;

        if (TimeScore > 380)
        {
            RankText.text = "S";
        }
        else if (TimeScore <= 380 && TimeScore > 280)
        {
            RankText.text = "A";
        }
        else if (TimeScore <= 280)
        {
            RankText.text = "B";
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

    private void Awake() 
    {
        rectTransform.anchoredPosition = new Vector3(0, 880, 0);    
    }


    void Start()
    {
        initialize();
        Day1LapApper();
        Day2LapApper();
        Day3LapApper();
        TotalLapApper();

        Debug.Log("RankScore: " + RankScore);
        Debug.Log("TimeScore: " + TimeScore);
        playfabLogin.Login();

        if(playfabLogin.Login() == true)
        {
            // ShowWindow();
            Invoke("ShowWindow", 2.5f);
        }
    }

    void ShowWindow()
    {
        rectTransform.DOLocalMoveY(0f, 2f).SetEase(Ease.OutBounce);
    }
}
