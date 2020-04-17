using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour, IDamageable
{
    public int dimonds;

    // Get handle to rigidbody
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 2.5f;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private bool _grounded = false;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() )
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        // Horizontal input for left/right
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");//
        _grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if(move < 0)
        {
            Flip(false);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded())
        {
            //jump!
            _playerAnim.JumpAnim(true);
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);
    }

    public void changeJumpForce(float jump)
    {
        _jumpForce = jump;
    }
    bool IsGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitinfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.JumpAnim(false);
                return true;
            }
        }
        return false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;

            // handling sword affect change side
            _swordArcSprite.flipX = false;

            _swordArcSprite.flipY = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;

            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }
        Debug.Log("Player::Damage()");
        Health--;
        _playerAnim.hit();
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            Death();
        }
    }

    public void Death()
    {
        _playerAnim.Death();
        StartCoroutine(PostDeathAnimationTimer());
    }
    public IEnumerator PostDeathAnimationTimer()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    public void AddGems(int amount)
    {
        dimonds += amount;
        UIManager.Instance.UpdateGemCount(dimonds);
    }

    public void MinusGems(int amount)
    {
        dimonds -= amount;
        UIManager.Instance.UpdateGemCount(dimonds);
    }
}
