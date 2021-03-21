using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public float damagedCd = 0f;

    [HideInInspector]public int currentHealth;
    private bool canBeDamaged = true;

    public UnityEvent<int, int> onHealthChange;
    public UnityEvent<float, Vector2> onDamaged;
    public UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    public void GetDamaged(int damage, float knockback, Vector2 enemy)
    {
        //Debug.Log("Enter");
        if (canBeDamaged)
        {
            HealthChange(-damage);
            onDamaged?.Invoke(knockback, enemy);
            StartCoroutine(CrDamagedCD());
        }
    }

    public void HealthChange(int amount)
    {
        currentHealth += amount;
        onHealthChange?.Invoke(currentHealth, totalHealth);
        if (currentHealth <= 0)
        {
            Killed();
        }
    }

    public void Killed()
    {
        onDeath?.Invoke();
    }

    IEnumerator CrDamagedCD()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(damagedCd);
        canBeDamaged = true;
    }
}
