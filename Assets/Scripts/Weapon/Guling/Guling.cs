using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guling : Weapon
{
    public float downTime;
    Animator animator;
    Coroutine crDownTime;
    float attackCdTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

    IEnumerator CrDownTime()
    {
        //animator.ResetTrigger("Attack");
        yield return new WaitForSeconds(downTime);
        animator.SetTrigger("Recover");
        crDownTime = null;
    }
}
