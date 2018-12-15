using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameManager gameManager;
    public float baseCooldown;
    public float baseStunned;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public float HealthPercentage
    {
        get { return Mathf.Max((float) health / (float) maxHealth , 0); }
    }

    [SerializeField] private int stagger;
    [SerializeField] private int maxStagger;
    public float StaggerPercentage
    {
        get { return Mathf.Max((float) stagger / (float) maxStagger, 0); }
    }

    private float attackDelay = 1;
    private float cooldown = 0;
    private float stunnedDuration = 0;
    private PatternColor currentAttack;

    private void Update()
    {
        if (stunnedDuration > 0)
        {
            stunnedDuration -= Time.deltaTime;

            if (stunnedDuration <= 0)
            {
                //Remove Stun
                SelectAttack();
            }
            return;
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                SelectAttack();
            }
            return;
        }

        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;

            if(attackDelay <= 0)
            {
                Attack();
            }
        }
    }

    public void SelectAttack()
    {
        PatternColor randomColor;
        while (true)
        {
            randomColor = (PatternColor)Random.Range(0, (int)PatternColor.Blue);

            if(randomColor != PatternColor.None)
            {
                break;
            }
        }

        currentAttack = randomColor;
        
        // Charge
        // Show Timer
    }

    public void AttackNow()
    {
        attackDelay = 0;

        Attack();
    }

    public void Attack()
    {
        int damage;
        switch (currentAttack)
        {
            default:
                damage = 10;
                break;
        }

        gameManager.DamagePlayer(damage, currentAttack);

        cooldown = baseCooldown;
    }

    public void Damage(int value)
    {
        health -= value;

        if (health < 0)
        {
            // Health below 0
            Debug.Log("Enemy Dead");
        }
    }

    public void ReduceStagger(int value)
    {
        stagger -= value;

        if (stagger < 0)
        {
            // Stagger below 0
            Debug.Log("Enemy Stunned");
            
            stunnedDuration = baseStunned;
        }
    }
}
