using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float damage, knockBack, stopDistance;
    public float moveSpeed, moveTime, movePause;

    Transform player;
    Rigidbody2D rb2D;
    Animator animator;
    Vector2 direction = Vector2.zero;
    private float newMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartJump();
    }

    void StartJump()
    {
        if (player)
        {
            direction = GotoPlayer.GetDirection(player.position, transform.position, stopDistance);
            if (direction.magnitude < moveSpeed * moveTime)
                newMoveSpeed = direction.magnitude;
            else
                newMoveSpeed = moveSpeed;
            direction = direction.normalized;
            if (Mathf.Sign(direction.x) != transform.localScale.x)
                transform.localScale = new Vector3(transform.localScale.x * -1f, 1f, 1f);
            if (direction != Vector2.zero)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    IEnumerator SlimeMoving()
    {
        animator.ResetTrigger("Jump");
        float movingTime = moveTime;
        while (movingTime > 0f)
        {
            rb2D.MovePosition(rb2D.position + direction * newMoveSpeed * Time.fixedDeltaTime);
            movingTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        animator.SetTrigger("EndJump");
    }

    IEnumerator MovingCD()
    {
        animator.ResetTrigger("EndJump");
        yield return new WaitForSeconds(movePause);
        StartJump();
    }
}
