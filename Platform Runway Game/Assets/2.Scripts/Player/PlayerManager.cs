using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 2f;

    [SerializeField]
    int maxJumpCount = 2;
    float jumpCount;

    [SerializeField]
    Image[] hpImages;
    
    bool isGrounded = false;

    Rigidbody2D rb;

    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        HandleJump();
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            JumpBtn();
        }
    }

    public void JumpBtn()
    {
        if (jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            jumpCount++;

            anim.SetTrigger("Jump");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;

            anim.SetTrigger("Run");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void SetHP(int index, bool isActive)
    {
        hpImages[index].color = isActive == true ? Color.white : Color.black;
    }

    void Hit()
    {
        GameManager.Instance.lives -= 1;

        if(GameManager.Instance.lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
