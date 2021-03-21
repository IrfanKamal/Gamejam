using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bantal : Weapon
{
    bool shielding = false;
    public Vector3 shieldingOffset;
    public Collider2D shield, deflect;
    public float shieldKnockback;
    private int currentDamage = 0;
    private float currentKnockback;
    Animator animator;
    Transform player;
    SpriteRenderer sprite;
    public AudioSource attackSound, shieldSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = transform.parent;
        canAtttack = false;
        currentKnockback = shieldKnockback;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shielding = true;
            transform.position = player.position + shieldingOffset * player.localScale.x;
            animator.SetBool("Shield", true);
            shield.enabled = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            shielding = false;
            animator.SetBool("Shield", false);
            transform.position = player.position;
            shield.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && shielding && canAtttack)
        {
            animator.SetTrigger("Deflect");
            canAtttack = false;
        }
    }
    private void LateUpdate()
    {
        if (!shielding)
        {
            sprite.sortingOrder--;
        }
    }

    public void DeflectHitbox()
    {
        if (deflect.enabled == false)
        {
            deflect.enabled = true;
            currentDamage = damage;
            currentKnockback = knockback;
        }
        else
        {
            deflect.enabled = false;
            currentDamage = 0;
            currentKnockback = shieldKnockback;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().GetDamaged(currentDamage, currentKnockback, transform.position);
            if (currentDamage == 0)
            {
                canAtttack = true;
                shieldSound.Play();
            }
            else
                attackSound.Play();
        }
    }
}
