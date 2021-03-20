using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float knockTime;
    Rigidbody2D rb2D;
    Animator animator;
    Vector2 movement;

    Coroutine crKnocked = null;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero && canMove)
        {
            animator.SetBool("Running", true);
            rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);
            if (transform.localScale.x != movement.x * -1f && movement.x != 0f)
            {
                transform.localScale = new Vector3(movement.x * -1f, 1f, 1f);
            }
        }
        else
            animator.SetBool("Running", false);
    }

    public void KnockPlayer(float knockback, Vector2 enemy)
    {
        if (crKnocked != null)
            StopCoroutine(crKnocked);
        Vector2 direction = new Vector2(transform.position.x - enemy.x, transform.position.y - enemy.y).normalized;
        crKnocked = StartCoroutine(KnockingPlayer(knockback, direction));
    }

    public IEnumerator KnockingPlayer(float knockback, Vector2 direction)
    {
        canMove = false;
        float timeKnocked = knockTime;
        while (timeKnocked > 0f)
        {
            timeKnocked -= Time.fixedDeltaTime;
            rb2D.MovePosition(rb2D.position + direction * knockback * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        canMove = true;
        crKnocked = null;
    }
}
