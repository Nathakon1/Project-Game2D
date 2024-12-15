using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    AudioManager audioManager;
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public GameObject HP3;
    public GameObject HP2;
    public GameObject finalStar;

    public float x;
    public int speed = 2;
    public int jumpingPower = 5;
    public bool walk = false;
    public bool run = false;
    public int hp;

    public bool isJump;
    public bool isHit;
    public bool dead;

    private bool isTakingDamage;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.1f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        HP3.SetActive(true);
        HP2.SetActive(true);
        hp = 3;
        dead = false;
        isTakingDamage = false;
        Transform transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (finalStar.activeSelf)
        {
            SceneManager.LoadSceneAsync(5);
        }

        if (hp == 2)
        {
            HP3.SetActive(false);
        }
        else if (hp == 1)
        {
            HP2.SetActive(false);
        }

        if (dead)
        {
            SceneManager.LoadSceneAsync(4);
        }

        x = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        if (Input.GetButtonDown("Jump") && isJump)
        {
            audioManager.PlaySFX(audioManager.hit);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("Jump", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }

        if (x != 0 && run && isJump)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
            speed = 4;
        }
        else if (x != 0 && !run && isJump)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
            speed = 2;
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }

        if (x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !isTakingDamage)
        {
            isHit = true;
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            StartCoroutine(Knockback(knockbackDirection));
            StartCoroutine(TakeDamageOverTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
            anim.SetBool("Jump", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isHit = false;
        }
    }

    private IEnumerator TakeDamageOverTime()
    {
        isTakingDamage = true;
        while (isHit && hp > 0)
        {
            audioManager.PlaySFX(audioManager.jump);
            hp -= 1;
            if (hp <= 0)
            {
                dead = true;
            }
            yield return new WaitForSeconds(0.5f);
        }
        isTakingDamage = false;
    }

    private IEnumerator Knockback(Vector2 direction)
    {
        float timer = 0;
        direction.y = 0;
        direction.Normalize();

        while (timer < knockbackDuration)
        {
            rb.AddForce(new Vector2(direction.x * knockbackForce, 0), ForceMode2D.Impulse);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
