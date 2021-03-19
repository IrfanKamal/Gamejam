using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rb2D;
    Animator animator;
    Vector2 movement;

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
        if (movement != Vector2.zero)
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
}
