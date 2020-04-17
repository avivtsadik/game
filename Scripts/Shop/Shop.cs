using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.dimonds);
            }
            UIManager.Instance.unableShopSelection();
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        // 0 = flame sword
        // 1 = boots of flight 
        // 2 = key to castle

        switch(item)
        {
            case 0:
                UIManager.Instance.enableShopSelection();
                UIManager.Instance.UpdateShopSelection(-54);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.enableShopSelection();
                UIManager.Instance.UpdateShopSelection(-170);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.enableShopSelection();
                UIManager.Instance.UpdateShopSelection(-283);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    public void ButItem()
    {
        if (_player.dimonds >= currentItemCost)
        {
            if (currentSelectedItem == 2 && !GameManager.Instance.HasKeyToCastle)
            {
                GameManager.Instance.Player.MinusGems(currentItemCost);
                GameManager.Instance.HasKeyToCastle = true;
                UIManager.Instance.showKeyPicture();
            }
            else if (currentSelectedItem == 1 && !GameManager.Instance.HasImprovedJump)
            {
                GameManager.Instance.Player.MinusGems(currentItemCost);
                GameManager.Instance.HasImprovedJump = true;
                UIManager.Instance.showWingsPicture();
            }
            else if (currentSelectedItem == 0 && !GameManager.Instance.HasSwordImprove)
            {
                GameManager.Instance.Player.MinusGems(currentItemCost);
                GameManager.Instance.HasSwordImprove = true;
                UIManager.Instance.showSwordPicture();
            }
            //shopPanel.SetActive(false);
        }
        else
        {
            UIManager.Instance.notEnoughMoney.SetActive(true);
            StartCoroutine(suspendForTwoSec());
        }

    }
    private IEnumerator suspendForTwoSec()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.notEnoughMoney.SetActive(false);
    }
}
