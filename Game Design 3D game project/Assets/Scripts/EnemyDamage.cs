using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private Collider enemyCollider;

    [SerializeField] private float damageToDeal;

    void Start()
    {
        enemyCollider = GetComponent<Collider>();
    }

    void OnCollisionStay(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health == null) return;

        health.ChangeHealth(-damageToDeal);
    }
}
