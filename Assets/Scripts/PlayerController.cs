using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float speed = 5.0f;

    private float movementX = 0.0f;
    private Vector2 vec = Vector2.zero;
    private bool facingLeft = false;
    private bool jumping = false;
    private bool standing = false;
    private int score = 0;


    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    // }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        Vector2 scale = transform.localScale;
        if (rb.velocity.x < -0.01f && !facingLeft)
        {
            scale.x = Mathf.Abs(scale.x) * -1.0f;
            facingLeft = true;
        }
        if (rb.velocity.x > 0.01f && facingLeft)
        {
            scale.x = Mathf.Abs(scale.x);
            facingLeft = false;
        }
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(movementX, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref vec, 0.05f);

        if (standing)
        {
            float angle = Mathf.Lerp(rb.rotation, 0.0f, 0.1f);
            rb.SetRotation(angle);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x * speed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (jumping) return;

        rb.AddForce(new Vector2(0.0f, 8.0f), ForceMode2D.Impulse);
    }

    public void OnStandUp(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        if (value == 1)
        {
            standing = true;
        }
        else
        {
            standing = false;
        }
    }

    public void OnExit()
    {
        Application.Quit();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            jumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            jumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            score += 1;
        }
    }
}
