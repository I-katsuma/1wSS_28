using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField] private GameObject NameInputFieldPanel;

    [SerializeField] private GameObject LeaderBoardTable;
    
    [SerializeField] private InputField nameInput; 

    //private int minTrank;
    //private int secTrank;

    private readonly string isRankingName = "MoonCamperRanking";

    [SerializeField]
    private Text errorText;
    

    [SerializeField]
    private ResultManager resultManager;

    public GameObject rowPrefab;
    public Transform rowsParent;

    private void Start()
    {
        LeaderBoardTable.SetActive(true);
        NameInputFieldPanel.SetActive(true);
        //minTrank = resultManager.minTvalue;
        //secTrank = resultManager.secTvalue;

        
        ErrorSet("");
        //Login();
    }

    
    public void ErrorSet(string etxt)
    {
        //errorText.SetActive(x);
        errorText.text = etxt;
    }
    

    public void CloseMiniTablePanel()
    {
        NameInputFieldPanel.SetActive(false);
        GetLeaderBoard();
        
    }

    public bool Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnLoginError);

        return true;
    }

    void OnSuccess(LoginResult result)
    {
        
        //GetLeaderBoard();
        //LeaderBoardTable.SetActive(false);

        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            SendLeaderBoard(resultManager.TimeScore); // スコア送信
            NameInputFieldPanel.SetActive(false);
        }

        if(name == null)
        {
            NameInputFieldPanel.SetActive(true);
        }

        Debug.Log("ログイン成功/アカウント作成");
       
    }

    void OnLoginError(PlayFabError error)
    {
        Debug.Log("ログイン失敗");
        //errorText.SetActive(true);
        ErrorSet("LOGIN ERROR");
    }

    public void SubmitNameButton() // 名前
    {
        // ユーザー名の更新
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnSubmitError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name");
        SendLeaderBoard(resultManager.TimeScore); // スコア送信
        NameInputFieldPanel.SetActive(false);

    }

    void OnSubmitError(PlayFabError error)
    {
        ErrorSet("UserName Display Error");
    }

    
    public void SendLeaderBoard(int score) // スコア送信
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = isRankingName, Value = score, }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnSendLeaderBoardError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"リーダーボードの送信に成功");
        GetLeaderBoard();
    }

    void OnSendLeaderBoardError(PlayFabError error)
    {
        ErrorSet("SEND LEADERBOARD ERROR");
    }

    public void SendLBbtn() // ボタン用スコア送信
    {
        SendLeaderBoard(resultManager.TimeScore);
    }


    public void GetLeaderBoard() // リーダーボード更新
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = isRankingName,
            StartPosition = 0,
            MaxResultsCount = 20
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnGetLeaderBoardError);
    }

    void OnGetLeaderBoardError(PlayFabError error)
    {
        ErrorSet("GET READERBOARD ERROR");
    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();

            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName; // item.PlayFabId;
            texts[2].text = Rank(item.StatValue);

            int num = 500 - item.StatValue;
            var span = new TimeSpan(0, 0, num);
            texts[3].text = span.ToString(@"mm\:ss");
            Debug.Log("リーダーボートの取得に成功");
        }
    }

    private string Rank(int value)
    {
        string rank = null;

        if (value > 380)
        {
            rank = "S";
        }
        else if (value <= 380 && value > 280)
        {
            rank = "A";
        }
        else if (value <= 280)
        {
            rank = "B";
        }

        return rank;
    }

    /*
    public void SaveAppearance() 
    {
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, int> {
                {"minT", minTrank},
                {"secT", secTrank}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDateSend, OnError);
    }

    void OnDateSend(UpdateUserDataResult result)
    {
        Debug.Log("ユーザーデータ送信成功！");
        GetAppearrance();
    }

    public void GetAppearrance()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result)
    {
        minTvalue = result.Data["minT"].Value;
        secTvalue = result.Data["secT"].Value;

        Debug.Log("ユーザーデータを受け取りました!");
        Debug.Log("minT: " + result.Data["minT"].Value + " secT: " + result.Data["secT"].Value);
    }
    */
}
