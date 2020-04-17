using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour
{
  public void ShowRewardedAd()
    {
        Debug.Log("Showing Rewarded add");
        // check if the advertisment is ready (rewardedVideo)
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };
            Advertisement.Show("rewardedVideo", options);
        }
    }
    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished!");
                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.dimonds);
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped Ad!");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Failed!");
                break;
        }
    }
}
