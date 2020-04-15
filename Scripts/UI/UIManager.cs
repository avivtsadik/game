using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null!");
            }
            return _instance;
        }
    }

    public TextMeshProUGUI playerGemCountText;
    public Image selectionImg;
    public TextMeshProUGUI gemCountText;
    public GameObject livesImages;
    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + " G";
    }

    public void UpdateShopSelection(int ypos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, ypos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count + " G";
    }

    public void UpdateLives(int livesRemaining)
    {
        if (livesRemaining >= 0)
        {
            livesImages.transform.GetChild(livesRemaining).GetComponent<Image>().enabled = false;
        }
    }

}
