using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterAd : MonoBehaviour
{
    //InterstitialAd interstitialAd;
    RewardedAd rewardedAd;
    bool isGo = false;
    public bool IsGo { get { return isGo; } }


    const string interstitialUnitAd = "ca - app - pub - 3940256099942544 / 5354046379";
    const string rewardedUnitId = "ca-app-pub-3940256099942544/5354046379";

    private void OnEnable()
    {
        //interstitialAd = new InterstitialAd(interstitialUnitAd);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //interstitialAd.LoadAd(adRequest);

        rewardedAd = new RewardedAd(rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);

        isGo = false;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    private void OnDisable()
    {
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
    }

    public void Show()
    {
        //if(interstitialAd.IsLoaded())interstitialAd.Show();
        if(rewardedAd.IsLoaded())rewardedAd.Show();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        isGo = true;
    }
}
