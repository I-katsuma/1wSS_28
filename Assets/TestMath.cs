using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestMath : MonoBehaviour
{

    private void Start() 
    {
        var seconds = 210;
        var span = new TimeSpan(0, 0, seconds);

        var hhmmss = span.ToString(@"mm\:ss");
        Debug.Log(hhmmss);
    }


    void TimeMath()
    {
        int minTotal = 2;
        int SecondsTotal = 0;

        /// <summary>
        /// 秒の処理
        /// </summary>
        double numA = SecondsTotal / 60.0;
        // 桁数調整
        double FLnum = Mathf.Floor((float)numA * Mathf.Pow(10, 3)) / Mathf.Pow(10, 3);

        Debug.Log("numA: " + numA);       
        Debug.Log("FLnum: " + FLnum);

        // 行程A(秒数)　小数点部分のみ
        float value = (float)FLnum;
        value = value - Mathf.Floor(value);
        Debug.Log("Value: " + value);
        
        double FLnum2 = Mathf.Floor(value * Mathf.Pow(10, 3)) / Mathf.Pow(10, 3);
        Debug.Log("FLnum2: " + FLnum2);
        FLnum2 *= 60.0f;
        Debug.Log("FLnum2*60: " + FLnum2);
        // 四捨五入して整数化
        var RDnum = Mathf.Round((float)FLnum2);

        Debug.Log("RDnum: " + RDnum);

        // 行程B(分数) numAを整数のみに
        var numB = Mathf.Floor((float)numA);
        Debug.Log("numB: " + numB);

        // まとめ
        Debug.Log("分: " + (numB + minTotal) + " 秒: " + RDnum);
        // 点数化
        Debug.Log( ((numB + minTotal) * 100) + RDnum + "点" );

    }

    
}
