using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (GameManager.Instance.HasImprovedJump)
                {
                    player.changeJumpForce(10.0f);
                    UIManager.Instance.hasJumpMessage.SetActive(true);
                    StartCoroutine(changeJumpForFiveSec(player));
                }
                else
                {
                    UIManager.Instance.noImprovedJump.SetActive(true);
                }
            }
        }
    }

    private IEnumerator changeJumpForFiveSec(Player player)
    {
        yield return new WaitForSeconds(7.0f);
        player.changeJumpForce(5.0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.noImprovedJump.SetActive(false);
            UIManager.Instance.hasJumpMessage.SetActive(false);
        }
    }
}
