using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Slider playerHPBar;
    [SerializeField] private Slider enemyHPBar;
    [SerializeField] private Slider enemyStaggerBar;

    public void UpdatePlayerHPBar(float percentage)
    {
        playerHPBar.value = percentage;
    }

    public void UpdateEnemyHPBar(float percentage)
    {
        enemyHPBar.value = percentage;
    }

    public void UpdateEnemyStaggerBar(float percentage)
    {
        enemyStaggerBar.value = percentage;
    }
}
