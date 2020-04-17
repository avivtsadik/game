using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy, IDamageable
{
    public int Health { get; set;}
    
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }
    public void Damage()
    {
        if (isDead)
            return;
        Debug.Log("BigEnemy::Damage!");
        if (GameManager.Instance.HasSwordImprove)
            Health -= 5;
        else
            Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            // disapear the BigEnemy
            StartCoroutine(PostDeathAnimationTimer());
        }
    }
}
