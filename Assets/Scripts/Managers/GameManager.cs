using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager {

    private PatternRecognition patternRecognition;
    [SerializeField] private UIController uiController;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    public void Init()
    {
        patternRecognition = gameObject.GetComponent<PatternRecognition>();

        patternRecognition.Init();
    }

    #region Player Functions
    public void DamagePlayer(int value)
    {
        player.Damage(value);
    }    
    #endregion

    #region Enemy Functions
    public void DamageEnemyHP(int value)
    {
        enemy.Damage(value);
    }

    public void DamageEnemyStagger(int value)
    {
        enemy.ReduceStagger(value);
    }
    #endregion

}
