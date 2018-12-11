using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public float HealthPercentage
    {
        get { return Mathf.Max(health / maxHealth, 0); }
    }
    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
