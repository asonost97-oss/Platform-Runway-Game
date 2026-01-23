using System.Collections;
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

    const int hpMax = 3; // 플레이어 HP
    int currentHP;

    bool isGrounded = false;
    bool isInvincible = false; // 무적 상태

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        // HP 초기화
        currentHP = hpMax;
        UpdateHPDisplay();
    }

    void Update()
    {
        // 게임이 Play 상태일 때만 동작
        if (GameManager.Instance != null && GameManager.Instance.state == GameState.Play)
        {
            HandleJump();
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 무적 상태일 때는 데미지를 받지 않음
        if (isInvincible)
            return;

        ObstacleManager obstacle = collision.gameObject.GetComponent<ObstacleManager>();
        if (obstacle != null)
        {
            Hit(obstacle.damage);
        }
    }

    public void SetHP(int index, bool isActive)
    {
        if (index >= 0 && index < hpImages.Length)
        {
            hpImages[index].color = isActive == true ? Color.white : Color.black;
        }
    }

    void UpdateHPDisplay()
    {
        for (int i = 0; i < hpImages.Length; i++)
        {
            SetHP(i, i < currentHP);
        }
    }

    public void Hit(int damage)
    {
        currentHP -= damage;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        UpdateHPDisplay();

        // 무적 상태 시작 (0.5초)
        isInvincible = true;
        StopCoroutine("InvincibleTimer");
        StartCoroutine("InvincibleTimer");

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        for (int i = 0; i < 3; i++)
        {
            color.a = 0.4f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);

            color.a = 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(0.6f);
        isInvincible = false;
    }
}
