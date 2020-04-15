using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool isDead = false;
    protected bool isHit = false;
    protected Player player;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    protected IEnumerator PostDeathAnimationTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("InCombat"))
        {
            return;
        }

        if (!isDead)
            Movement();
    }

    public virtual void Movement()
    {
        if (Vector3.Distance(currentTarget, pointA.position) < 0.001f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (Vector3.Distance(transform.position, pointA.position) < 0.001f)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (Vector3.Distance(transform.position, pointB.position) < 0.001f)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        // checking if the player is far and no longer in battle so the enemy can move regular
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        // to follow the player direction
        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (direction.x > 0 && anim.GetBool("InCombat"))
        {
            // face right
            sprite.flipX = false;
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0 && anim.GetBool("InCombat"))
        {
            // face left
            sprite.flipX = true;
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
