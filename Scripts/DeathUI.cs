using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.UpdateLives(3);
                UIManager.Instance.UpdateLives(2);
                UIManager.Instance.UpdateLives(1);
                UIManager.Instance.UpdateLives(0);
                player.Death();
                UIManager.Instance.deathPanel.SetActive(true);
            }
        }
    }
}
