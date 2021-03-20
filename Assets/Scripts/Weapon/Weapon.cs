using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector2 offset;
    public float attackCd;
    public int damage;
    public float knockback;
    protected bool canAtttack = true;

}
