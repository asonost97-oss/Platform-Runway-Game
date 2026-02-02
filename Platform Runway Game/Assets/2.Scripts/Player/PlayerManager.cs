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

    [SerializeField]
    float maxTime = 30;
    float currTime;

    public float MaxTime => maxTime;
    public float CurrTime => currTime;

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

        // Timer 초기화
        currTime = maxTime;

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

            // Timer 감소
            currTime -= Time.deltaTime;
            if (currTime < 0f) currTime = 0f;

            if (currTime <= 0f)
            {
                if (GameManager.Instance != null)
                    GameManager.Instance.state = GameState.Dead;
            }
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

        //Lose SFX 재생
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(SFX.Select);
            Debug.Log("Lose SFX 재생!");
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
    
    public void AddHP(int amount)
    {
        currentHP = Mathf.Min(hpMax, currentHP + amount);
        UpdateHPDisplay();
    }

    public void Hit(int damage)
    {
        // Hit SFX 재생
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(SFX.Hit);
        }

        currentHP -= damage;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        UpdateHPDisplay();

        // 무적 상태 시작 (0.6초)
        isInvincible = true;

        StopCoroutine("InvincibleTime");
        StartCoroutine("InvincibleTime");

        if (currentHP <= 0)
        {
            // 플레이어 사망 시 게임 종료 상태로 전환
            if (GameManager.Instance != null)
                GameManager.Instance.state = GameState.Dead;
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

    IEnumerator InvincibleTime()
    {
        yield return new WaitForSeconds(0.6f);
        isInvincible = false;
    }

    // 플레이어에게 시간 추가 (골드 수집 등)
    public void AddTime(float seconds)
    {
        currTime = Mathf.Min(maxTime, currTime + seconds);
    }
}
