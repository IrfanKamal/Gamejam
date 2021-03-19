using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bantal : Weapon
{
    bool shielding = false;
    public Vector3 shieldingOffset;
    Animator animator;
    Transform player;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = transform.parent;
        canAtttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shielding = true;
            transform.position = player.position + shieldingOffset * player.localScale.x;
            animator.SetBool("Shield", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            shielding = false;
            animator.SetBool("Shield", false);
            transform.position = player.position;
        }

        if (Input.GetMouseButtonDown(0) && shielding)
        {
            animator.SetTrigger("Deflect");
        }
    }
    private void LateUpdate()
    {
        if (!shielding)
        {
            sprite.sortingOrder--;
        }
    }
}
