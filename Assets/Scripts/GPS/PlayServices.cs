using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class PlayServices : Singleton<PlayServices>
{
    private string _leaderboardID = "CgkIyMm-4-IIEAIQEQ";
    private string _mStatusText = "Ready.";
   
    void Start()
    {
        Configurate();
        DoAuthenticate();
    }
    private void Configurate()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
    }
    private void DoAuthenticate()
    {
        try
        {            
            PlayGamesPlatform.Activate();
            _mStatusText = "Authenticating...";
            PlayGamesPlatform.Instance.Authenticate((bool success, string message) => {
                if (success)
                {
                    _mStatusText = message + Social.localUser.userName;
                }
                else
                {
                    _mStatusText = message;
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    public void AddScoreToLeaderboard(int playerScore)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(playerScore, _leaderboardID, success => { });
        }
        else
        {
            DoAuthenticate();
            PlayGamesPlatform.Instance.ReportScore(playerScore, _leaderboardID, success => { });
        }
        Debug.Log("AddScoreToLeaderboard" + playerScore);
    }
    public void ShowLeaderboard()
    {
        AudioManager.Instance.PlayUIclick();
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            DoAuthenticate();
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        Debug.Log("ShowLeaderboard");
    }

    public void ShowAchievements()
    {
        AudioManager.Instance.PlayUIclick();
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            DoAuthenticate();
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        Debug.Log("ShowAchievementsUI");
    }

    public void UnlockAchievement(string achievementID)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress(achievementID, 100f, success => { });
        }
        else
        {
            DoAuthenticate();
            PlayGamesPlatform.Instance.ReportProgress(achievementID, 100f, success => { });
        }
        Debug.Log("UnlockAchievement " + achievementID);
    }
    public void Quit()
    {
        PlayGamesPlatform.Instance.SignOut(); 
    }
}