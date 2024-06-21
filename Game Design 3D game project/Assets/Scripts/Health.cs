using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    private bool invincible = false;

    [SerializeField] private float invincibilityTime;
    [SerializeField] private float invincibilityDelta;

    private HealthUi healthUi;
    [SerializeField] private GameObject gameOver;

    void Start()
    {
        healthUi = FindObjectOfType<HealthUi>();
        health = maxHealth;

        healthUi.UpdateHealthbar(health / maxHealth);
    }

    void Update()
    {
        if (gameObject.transform.position.y < -5 && health > 0)
        {
            ChangeHealth(-maxHealth);
        }
    }

    public void ChangeHealth(float amount)
    {
        if (invincible) return;

        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthUi.UpdateHealthbar(health / maxHealth);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    void Die()
    {
        Debug.Log("DIE");

        // might change this? not sure, it works for now
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        CursorManager.UnlockCursor();
    }

    IEnumerator Invincibility()
    {
        invincible = true;

        float invincibilityEnd = Time.time + invincibilityTime;

        while (Time.time < invincibilityEnd)
        {
            yield return new WaitForSeconds(invincibilityDelta);
        }

        invincible = false;
    }
}
