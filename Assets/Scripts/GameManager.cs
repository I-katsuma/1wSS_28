using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int nowItemCount;

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private bool istimerSwitch = false;

    public bool isStageClear = false;

    public int isOteTsuki { get; set; }

    public enum SCENE_STATE
    {
        TITLE,
        STORY,
        STAGE1,
        STAGE2,
        STAGE3,
        RESULT,
    }

    public SCENE_STATE state = SCENE_STATE.TITLE;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TimePause() // 時間停止
    {
        Time.timeScale = 0;
    }

    public void TimeResume() // 再開
    {
        Time.timeScale = 1;
    }

    public bool TimerSwitchCheck() // 確認用
    {
        return istimerSwitch;
    }

    public void TimerSwitchForced(bool flag) // オンオフを強制的に
    {
        istimerSwitch = flag;
    }

    public void TimerSwitchMethod() // タイマーオンオフSwitch
    {
        if (istimerSwitch == false)
        {
            istimerSwitch = true;
        }
        else if (istimerSwitch == true)
        {
            istimerSwitch = false;
        }
    }

    public void ItemCountMethod() // カウント増やす
    {
        nowItemCount++;
        Debug.Log("獲得アイテム数: " + nowItemCount);
    }

    public int CountChecker() // 現在のアイテム数を参照
    {
        if (state == SCENE_STATE.STAGE1)
        {
            if (nowItemCount == 3)
            {
                StageClear();
            }
        }
        else if (state == SCENE_STATE.STAGE2)
        {
            if (nowItemCount == 5)
            {
                StageClear();
            }
        }
        else if (state == SCENE_STATE.STAGE3)
        {
            if (nowItemCount == 7)
            {
                StageClear();
            }
        }
        return nowItemCount;
    }

    public void initialize() // 初期化
    {
        nowItemCount = 0;
        isOteTsuki = 0;
        isStageClear = false;
    }

    public bool ClearFlagChecker()
    {
        return isStageClear;
    }

    private void StageClear()
    {
        Debug.Log("ステージクリア");
        if (isStageClear == false)
        {
            isStageClear = true;
        }
    }

    public void SetPlayBTN(int stageNum)
    {
        switch (stageNum)
        {
            case 0:
                state = SCENE_STATE.TITLE;
                AudioManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
                initialize();
                break;
            case 1:
                state = SCENE_STATE.STAGE1;
                AudioManager.Instance.PlayBGM(BGMSoundData.BGM.STAGE1);
                break;
            case 2:
                state = SCENE_STATE.STAGE2;
                AudioManager.Instance.PlayBGM(BGMSoundData.BGM.STAGE2);
                break;
            case 3:
                state = SCENE_STATE.STAGE3;
                AudioManager.Instance.PlayBGM(BGMSoundData.BGM.STAGE3);
                break;
            case 4:
                state = SCENE_STATE.RESULT;
                AudioManager.Instance.PlayBGM(BGMSoundData.BGM.RESULT);
                break;
            case 5:
                state = SCENE_STATE.STORY;
                AudioManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();
        SetPlayBTN(0);
    }
}
