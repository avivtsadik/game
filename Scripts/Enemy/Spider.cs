using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {
    }

    public void Damage()
    {
        if (isDead)
            return;
        if (GameManager.Instance.HasSwordImprove)
            Health -= 5;
        else
            Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            StartCoroutine(PostDeathAnimationTimer());
        }
    }

    public override void Movement()
    {
        // sit still
    }

    public void Attack()
    {
        // making the acid affect
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
