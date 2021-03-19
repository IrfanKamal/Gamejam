using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceBullet : MonoBehaviour
{
    public float moveSpeed, moveTime;
    public Rigidbody2D rb2D;

    public IEnumerator Shooting()
    {
        rb2D.velocity = new Vector2(moveSpeed * transform.localScale.x, 0f);
        yield return new WaitForSeconds(moveTime);
        Destroy(gameObject);
    }
}
