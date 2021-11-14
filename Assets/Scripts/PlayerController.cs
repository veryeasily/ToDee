using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject winText;
    public float speed = 5.0f;

    private int score = 0;
    private bool jumping = false;
    private bool standing = false;
    private float movementX = 0.0f;
    private bool facingLeft = false;
    private bool liftingOff = false;
    private Vector2 vec = Vector2.zero;


    // // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
    }

    // // Update is called once per frame
    // void Update()
    // {
    // }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        if (rb.velocity.x < -0.01f && !facingLeft)
        {
            Flip();
        }
        else if (rb.velocity.x > 0.01f && facingLeft)
        {
            Flip();
        }
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

        if (liftingOff)
        {
            rb.AddForce(new Vector2(0.0f, 20.0f));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x * speed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        float value = context.ReadValue<float>();

        if (value == 1 && !jumping)
        {
            rb.AddForce(new Vector2(0.0f, 3.0f), ForceMode2D.Impulse);
            StartCoroutine(Jump());
        }
        else if (value == 0)
        {
            liftingOff = false;
        }
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            jumping = false;
            liftingOff = false;
        }
        if (other.collider.CompareTag("Win"))
        {
            winText.SetActive(true);
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

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 localScale = transform.localScale;
        float scaleX = localScale.x * -1.0f;
        transform.localScale = new Vector3(scaleX, localScale.y, localScale.z);
    }

    IEnumerator Jump()
    {
        jumping = true;
        liftingOff = true;
        yield return new WaitForSeconds(0.25f);
        liftingOff = false;
    }
}
