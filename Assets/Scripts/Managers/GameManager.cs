using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager {

    private PatternRecognition patternRecognition;
    [SerializeField] private UIController uiController;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    [SerializeField]
    private AudioManager audioManager;

    public void Init()
    {
        patternRecognition = gameObject.GetComponent<PatternRecognition>();
        audioManager = gameObject.GetComponent<AudioManager>();
        audioManager.BGMPlayer(audioManager.audioLibrary.background);

        patternRecognition.Init();

        enemy.gameManager = this;
        player.gameManager = this;
    }

    #region Player Functions
    public void DamagePlayer(int value, PatternColor color)
    {
        player.Damage(value, color);
        uiController.UpdatePlayerHPBar(player.HealthPercentage);
    }    

    public void SetDefend(Pattern pattern)
    {
        player.Defend(pattern);
        // Enemy Attack delay = 0
        enemy.AttackNow();
    }
    #endregion

    #region Enemy Functions
    public void DamageEnemyHP(int value)
    {
        enemy.Damage(value);
        uiController.UpdateEnemyHPBar(enemy.HealthPercentage);
    }

    public void DamageEnemyStagger(int value)
    {
        enemy.ReduceStagger(value);
        uiController.UpdateEnemyStaggerBar(enemy.StaggerPercentage);
    }
    #endregion

    // Call all animations in a beat
    public void BeatAnimation()
    {
        // Player animation
        player.PlayerAnimationIdle();
        // Enemy animation
    }
}
