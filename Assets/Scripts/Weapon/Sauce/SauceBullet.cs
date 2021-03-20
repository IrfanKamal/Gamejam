using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceBullet : MonoBehaviour
{
    public float moveSpeed, moveTime, knockback;
    public Rigidbody2D rb2D;
    public int damage;
    public Vector2 direction = Vector2.zero;
    Coroutine crShooting = null;

    public void Shooting()
    {
        crShooting = StartCoroutine(CrShooting());
    }

    public IEnumerator CrShooting()
    {
        rb2D.velocity = direction * moveSpeed;
        yield return new WaitForSeconds(moveTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().GetDamaged(damage, knockback, transform.position);
            StopCoroutine(crShooting);
            Destroy(gameObject);
        }
    }
}
