using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float knockBack, stopDistance;
    public float moveSpeed, moveTime, movePause;
    public float knockTime = 0.3f;
    public int damage;

    Transform player;
    Rigidbody2D rb2D;
    Animator animator;
    Vector2 direction = Vector2.zero;
    Coroutine crKnockback = null;
    private float newMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartJump();
        GetComponent<Health>().onDeath.AddListener(GameManager.gm.EnemyKilled);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enter");
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Enter");
            collision.gameObject.GetComponent<Health>().GetDamaged(damage, knockBack, transform.position);
        }
    }

    public void KnockBacked(float amount, Vector2 from)
    {
        if (crKnockback != null)
            StopCoroutine(CrKnockBacked(amount, from));
        StopAllCoroutines();
        animator.Play("Idle");
        crKnockback = StartCoroutine(CrKnockBacked(amount, from));
    }

    public IEnumerator CrKnockBacked(float amount, Vector2 from)
    {
        Vector2 direction = ((Vector2)transform.position - from).normalized;
        float timeKnockback = knockTime;
        while (timeKnockback > 0f)
        {
            timeKnockback -= Time.fixedDeltaTime;
            rb2D.MovePosition(rb2D.position + direction * amount * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        crKnockback = null;
        StartJump();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
