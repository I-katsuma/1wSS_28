using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAndScore : MonoBehaviour
{
    [SerializeField]
    private Text ItemCountText;

    [SerializeField]
    private int MaxItemCount;

    // タイマー用変数
    [SerializeField]
    private int minute;

    [SerializeField]
    private float seconds;

    // 前のUpdateの時の秒数
    private float oldSeconds;

    // タイマー表示用テキスト
    [SerializeField]
    private Text TimerText;

    [SerializeField]
    private Text ClearTimeText;

    // スタートボタンパネル
    [SerializeField]
    GameObject StartPanel;

    [SerializeField]
    GameObject GoalPanel;

    [SerializeField]
    Player player;

    private bool isClearCheck = false;

    void Start()
    {
        TimerInit();

        ItemCountText.text =
            GameManager.Instance.CountChecker().ToString() + "/" + MaxItemCount.ToString();

        StartPanel.SetActive(true);
    }

    public void StartButton()
    {
        StartPanel.SetActive(false);
        player.mode = Player.PLAYER_MODE.MOVE;
        GameManager.Instance.TimerSwitchMethod(); // タイムON
        AudioManager.Instance.PlaySE(SESoundData.SE.ButtonPush2);
    }

    private void TimerInit()
    {
        isClearCheck = false;
        StartPanel.SetActive(false);
        GoalPanel.SetActive(false);
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
    }

    public void TimerRecode()
    {
        int bestMinute = minute;
        float bestSeconds = seconds;

        if (GameManager.Instance.state == GameManager.SCENE_STATE.STAGE1)
        {
            PlayerPrefs.SetInt("BEST_MINUTE_01", bestMinute);
            PlayerPrefs.SetFloat("BEST_SECONDES_01", bestSeconds);
        }
        else if (GameManager.Instance.state == GameManager.SCENE_STATE.STAGE2)
        {
            PlayerPrefs.SetInt("BEST_MINUTE_02", bestMinute);
            PlayerPrefs.SetFloat("BEST_SECONDES_02", bestSeconds);
        }
        else if (GameManager.Instance.state == GameManager.SCENE_STATE.STAGE3)
        {
            PlayerPrefs.SetInt("BEST_MINUTE_03", bestMinute);
            PlayerPrefs.SetFloat("BEST_SECONDES_03", bestSeconds);
        }
        PlayerPrefs.Save();
    }

    public void ClearLapApper()
    {
        int clearM = 0;
        float clearS = 0;

        if (GameManager.Instance.state == GameManager.SCENE_STATE.STAGE1)
        {
            clearM = PlayerPrefs.GetInt("BEST_MINUTE_01", 0);
            clearS = PlayerPrefs.GetFloat("BEST_SECONDES_01", 0);
        }
        else if (GameManager.Instance.state == GameManager.SCENE_STATE.STAGE2)
        {
            clearM = PlayerPrefs.GetInt("BEST_MINUTE_02", 0);
            clearS = PlayerPrefs.GetFloat("BEST_SECONDES_02", 0);
        }
        else if (GameManager.Instance.state == GameManager.SCENE_STATE.STAGE3)
        {
            clearM = PlayerPrefs.GetInt("BEST_MINUTE_03", 0);
            clearS = PlayerPrefs.GetFloat("BEST_SECONDES_03", 0);
        }
        Debug.Log(clearM.ToString("00") + ":" + ((int)clearS).ToString("00"));
        ClearTimeText.text = clearM.ToString("00") + ":" + ((int)clearS).ToString("00");
    }

    private void Update()
    {
        if (GameManager.Instance.ClearFlagChecker())
        {
            if (isClearCheck)
            {
                // Updateから抜ける
                return;
            }
            else if (isClearCheck == false)
            {
                Debug.Log("checker");
                player.mode = Player.PLAYER_MODE.STOP;
                GameManager.Instance.TimerSwitchMethod(); // 時間とめ
                GoalPanel.SetActive(true);
                TimerRecode(); // 時間記録
                ClearLapApper(); // 記録表示
                isClearCheck = true; // クリアフラグをたてると…
                return;
            }
        }

        if (GameManager.Instance.TimerSwitchCheck() == true)
        {
            seconds += Time.deltaTime;
            if (seconds >= 60f)
            {
                minute++;
                seconds = seconds - 60;
            }
            // 値が変わった時だけテキストUIを更新
            if ((int)seconds != (int)oldSeconds)
            {
                TimerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
        }
    }

    public void UpdateItemCount()
    {
        ItemCountText.text =
            GameManager.Instance.CountChecker().ToString() + "/" + MaxItemCount.ToString();
    }
}
