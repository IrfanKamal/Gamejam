using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guling : Weapon
{
    public float downTime;
    public float hitboxTime = 0.1f;
    Animator animator;
    Coroutine crDownTime, crHitbox;
    PolygonCollider2D hitbox;
    float attackCdTime = 0f;
    public AudioSource attackSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAtttack)
        {
            canAtttack = false;
            attackCdTime = attackCd;
            animator.SetTrigger("Attack");
            //animator.ResetTrigger("Recover");
            if (crDownTime != null)
                StopCoroutine(crDownTime);
            crDownTime = StartCoroutine(CrDownTime());
        }
        if (attackCdTime > 0f)
        {
            attackCdTime -= Time.deltaTime;
        }
        else
            canAtttack = true;
    }

    public void CreateAttack()
    {
        if (crHitbox != null)
            StopCoroutine(crHitbox);
        crHitbox = StartCoroutine(CrHitbox());
    }

    IEnumerator CrHitbox()
    {
        hitbox.enabled = true;
        yield return new WaitForSeconds(hitboxTime);
        hitbox.enabled = false;
        crHitbox = null;
    }

    IEnumerator CrDownTime()
    {
        //animator.ResetTrigger("Attack");
        yield return new WaitForSeconds(downTime);
        animator.SetTrigger("Recover");
        crDownTime = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().GetDamaged(damage,knockback,transform.position);
            attackSound.Play();
        }
    }
}
