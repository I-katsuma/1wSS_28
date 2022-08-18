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

    private int minTrank;
    private int secTrank;

    private readonly string isRankingName = "MoonCamperRanking";

    [SerializeField]
    private GameObject errorText;

    [SerializeField]
    private ResultManager resultManager;

    public GameObject rowPrefab;
    public Transform rowsParent;

    private void Start()
    {
        LeaderBoardTable.SetActive(true);
        NameInputFieldPanel.SetActive(false);
        minTrank = resultManager.minTvalue;
        secTrank = resultManager.secTvalue;

        ErrorSet(false);
        //Login();
    }

    public void ErrorSet(bool x)
    {
        errorText.SetActive(x);
    }

    public void CloseMiniTablePanel()
    {
        NameInputFieldPanel.SetActive(false);
        
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        SendLeaderBoard(resultManager.RankScore);
        //GetLeaderBoard();
        //LeaderBoardTable.SetActive(false);

        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if(name == null)
        {
            NameInputFieldPanel.SetActive(true);
        }

        Debug.Log("ログイン成功/アカウント作成");
        /*
        if (resultManager.RankScore < 50 ||  resultManager.RankScore == 1000)
        {
            errorText.SetActive(true);
        }
        else
        {
            SendLeaderBoard(resultManager.RankScore);
            //SaveAppearance();
       }
       */
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("ログイン失敗");
        errorText.SetActive(true);
    }

    public void SubmitNameButton()
    {
        // ユーザー名の更新
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name");
        NameInputFieldPanel.SetActive(false);

    }



    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = isRankingName, Value = score, }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"リーダーボードの送信に成功");
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = isRankingName,
            StartPosition = 0,
            MaxResultsCount = 8
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {

            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();

            texts[0].text = item.Position + 1.ToString();
            texts[1].text = item.DisplayName; // item.PlayFabId;

            var span = new TimeSpan(0, 0, item.StatValue);
            texts[2].text = span.ToString(@"mm\:ss");
            Debug.Log("リーダーボートの取得に成功");
        }
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
