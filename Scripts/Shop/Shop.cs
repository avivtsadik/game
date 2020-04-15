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
                UIManager.Instance.UpdateShopSelection(-54);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-170);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
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
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.dimonds -= currentItemCost;
            Debug.Log("Purchesed " + currentSelectedItem);
            Debug.Log("Remaining gems: " + _player.dimonds);
            shopPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough money!");
            shopPanel.SetActive(false);
        }

    }
}
