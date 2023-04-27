using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("stats")]
    public float moveSpeed;
    public int curHp;
    public int maxHp;
    public int xpToGive;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    private Player player;

    [Header("attack")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;

    //components
    private Rigidbody2D rig;

    void Awake ()
    {

        // Get the player target
        player = FindObjectOfType<Player>();

        // Get the rigidbody component
        rig = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        // Get the distance between the player and the enemy
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // If the distance is less than the attack range
        if(distance <= attackRange)
        {
           // attack the player
        if(Time.time - lastAttackTime >= attackRate)
        {
            Attack();
        }
           rig.velocity = Vector2.zero;
        }
        // If the distance is less than the chase range
        else if(distance <= chaseRange)
        {
            // chase the player
            Chase();
        }
        else
        {
            // stop moving

            rig.velocity = Vector2.zero;
        }

    }

    void Attack()
    {
        lastAttackTime = Time.time;

        player.TakeDamage(damage);
    }

    void Chase()
    {
        // Get the direction to the player
        Vector2 dir = (player.transform.position - transform.position).normalized;

        // Move towards the player
        rig.velocity = dir * moveSpeed;
    }

    public void TakeDamage (int damageTaken)
    {
        curHp -= damageTaken;

        if(curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Give the player xp
        player.AddXp(xpToGive);
        
        Destroy(gameObject);
    }
}
