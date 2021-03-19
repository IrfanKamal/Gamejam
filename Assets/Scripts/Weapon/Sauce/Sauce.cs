using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce : Weapon
{
    public GameObject sauceBulletPrefab;
    public float chargeTime;

    Animator animator;
    Coroutine crCharging, crAttackCD = null;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAtttack)
        {
            canAtttack = false;
            crCharging = StartCoroutine(CrCharging());
        }
        if (Input.GetMouseButtonUp(0) && crAttackCD == null)
        {
            StopCoroutine(crCharging);
            crCharging = null;
            canAtttack = true;
            animator.SetBool("Charge", false);
        }
    }
    private void LateUpdate()
    {
        sprite.sortingOrder++;
    }
    IEnumerator CrCharging()
    {
        animator.SetBool("Charge", true);
        yield return new WaitForSeconds(chargeTime);
        if (sauceBulletPrefab)
        {
            GameObject newBullet = Instantiate(sauceBulletPrefab);
            newBullet.transform.position = transform.position;
            newBullet.transform.localScale = transform.parent.localScale * -1f;
            sprite.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            StartCoroutine(newBullet.GetComponent<SauceBullet>().Shooting());
            sprite.color = new Color(1f, 1f, 1f, 0f);
            crAttackCD = StartCoroutine(AttackCD());
        }
        animator.SetBool("Charge", false);
        crCharging = null;
    }

    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackCd);
        sprite.color = new Color(1f, 1f, 1f, 1f);
        canAtttack = true;
        crAttackCD = null;
    }
}
