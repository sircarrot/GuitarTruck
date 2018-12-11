using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public float HealthPercentage
    {
        get { return Mathf.Max(health / maxHealth, 0); }
    }

    [SerializeField] private int stagger;
    [SerializeField] private int maxStagger;
    public float StaggerPercentage
    {
        get { return Mathf.Max(stagger / maxStagger, 0); }
    }
    
    public void SelectAttack()
    {

    }

    public void Attack()
    {

    }

    public void Damage(int value)
    {
        health -= value;

        if (health < 0)
        {
            // Health below 0
        }
    }

    public void ReduceStagger(int value)
    {
        stagger -= value;

        if (stagger < 0)
        {
            // Stagger below 0
        }
    }


}
