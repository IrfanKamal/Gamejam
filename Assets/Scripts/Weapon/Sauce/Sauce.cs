using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce : Weapon
{
    public GameObject sauceBulletPrefab;
    public float chargeTime;
    public int bulletTotal;
    public float angleOffset;
    public Transform targetAngle;

    private int currentBullet;

    Animator animator;
    Coroutine crCharging = null;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currentBullet = bulletTotal;
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (canAtttack)
            {
                Attack(mousePosition);
            }
            else
            {
                crCharging = StartCoroutine(CrCharging());
            }
        }
        Vector2 angle = (mousePosition - (Vector2)transform.position).normalized;
        float zRotation = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        if (transform.parent.localScale.x == 1)
            zRotation += 180;
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation + angleOffset);
    }
    private void LateUpdate()
    {
        sprite.sortingOrder++;
    }
    IEnumerator CrCharging()
    {
        animator.SetBool("Charge", true);
        yield return new WaitForSeconds(chargeTime);
        canAtttack = true;
        currentBullet = bulletTotal;
        animator.SetBool("Charge", false);
        crCharging = null;
    }

    void Attack(Vector2 target)
    {
        animator.SetTrigger("Shoot");
        currentBullet--;
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        GameObject newBullet = Instantiate(sauceBulletPrefab);
        newBullet.transform.position = transform.position;
        newBullet.transform.localScale = transform.parent.localScale;
        newBullet.transform.rotation = transform.rotation;
        newBullet.GetComponent<SauceBullet>().direction = direction;
        newBullet.GetComponent<SauceBullet>().Shooting();
        if (currentBullet <= 0)
        {
            canAtttack = false;
        }
    }
}
