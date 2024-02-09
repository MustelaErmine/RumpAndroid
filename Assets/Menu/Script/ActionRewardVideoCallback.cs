using AppodealAds.Unity.Common;
using System;

public class ActionRewardVideoCallback : IRewardedVideoAdListener
{
    Action onFinished;
    public ActionRewardVideoCallback(Action onFinished)
    {
        this.onFinished = onFinished;
    }
    public void onRewardedVideoClicked()
    {
    }

    public void onRewardedVideoClosed(bool finished)
    {
    }

    public void onRewardedVideoExpired()
    {
    }

    public void onRewardedVideoFailedToLoad()
    {
    }

    public void onRewardedVideoFinished(double amount, string name) => onFinished();

    public void onRewardedVideoLoaded(bool precache)
    {
    }

    public void onRewardedVideoShowFailed()
    {
    }

    public void onRewardedVideoShown()
    {
    }
}
