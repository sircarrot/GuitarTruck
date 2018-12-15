using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameManager gameManager;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public float HealthPercentage
    {
        get { return Mathf.Max((float) health / (float) maxHealth, 0); }
    }

    private Pattern pattern;

    public void Defend(Pattern pattern)
    {
        this.pattern = pattern;
    }

    public void Damage(int damage, PatternColor color)
    {
        if(pattern != null && pattern.color == color)
        {
            //Defend Animation

            pattern = null;
            gameManager.DamageEnemyStagger(pattern.stun);
            return;
        }


        health -= damage;

        if(health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
