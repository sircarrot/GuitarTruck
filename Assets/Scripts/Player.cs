using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;

    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
