using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (GameManager.Instance.HasKeyToCastle)
                {
                    UIManager.Instance.winPanel.SetActive(true);
                }
                else
                    UIManager.Instance.nokeyPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.nokeyPanel.SetActive(false);
        }
    }
}
