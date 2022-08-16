using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLogin : MonoBehaviour
{
    private readonly string isRankingName = "MoonCamperRanking";

    [SerializeField]
    private GameObject errorText;

    [SerializeField]
    private ResultManager resultManager;

    private void Start()
    {
        errorText.SetActive(false);
        //Login();
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("ログイン成功/アカウント作成");
        if (resultManager.RankScore < 50 ||  resultManager.RankScore == 1000)
        {
            errorText.SetActive(true);
        }
        else
        {
            SendLeaderBoard(resultManager.RankScore);
            SaveAppearance();
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("ログイン失敗");
        errorText.SetActive(true);
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
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(
                "Rank: "
                    + item.Position
                    + 1
                    + "  ID: "
                    + item.PlayFabId
                    + " Score: "
                    + item.StatValue
            );
        }
    }

    public void SaveAppearance() 
    {
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {"minT", resultManager.minTvalue.ToString()},
                {"secT", resultManager.secTvalue.ToString()}
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
        Debug.Log("ユーザーデータを受け取りました!");
        Debug.Log("minT: " + result.Data["minT"].Value + " secT: " + result.Data["secT"].Value);
    }
}
