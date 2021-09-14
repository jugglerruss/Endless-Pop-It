using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private LobbyUI _ui;
    [SerializeField] private string _banerUnitId;
    [SerializeField] private string _banerUnitIdTest;
    [SerializeField] private bool _isTest;
    private BannerView _bannerAd;

    private void Start()
    {
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {
        if (_bannerAd != null)
            _bannerAd.Destroy();
        _bannerAd = new BannerView(_isTest ? _banerUnitIdTest : _banerUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        _bannerAd.OnAdLoaded += HandleOnAdLoaded;
        _bannerAd.OnAdClosed += HandleOnAdClosed;
        _bannerAd.OnAdOpening += HandleOnAdOpened;
        _bannerAd.LoadAd(adRequest);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        _ui.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        _ui.Log("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        _ui.Log("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        _ui.Log("HandleAdClosed event received");
    }

}
