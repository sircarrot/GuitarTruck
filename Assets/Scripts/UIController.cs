using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Slider playerHPBar;
    [SerializeField] private Slider enemyHPBar;
    [SerializeField] private Slider enemyStaggerBar;

    [SerializeField] private List<Image> patternPlaying;

    [SerializeField] private Sprite clickedTrue;
    [SerializeField] private Sprite clickedFalse;

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

    public void ResetPattern()
    {
        foreach(Image image in patternPlaying)
        {
            image.sprite = null;
            image.gameObject.SetActive(false);
        }
    }

    //TODO: Fix compile error
    public void UpdatePattern(string pattern)
    {
        int count = 0;
        for(int i = 0; i < pattern.Length; ++i)
        {
            string character = pattern.Substring(i, 1);

            patternPlaying[count].gameObject.SetActive(true);
            if (character == "1")
            {
                patternPlaying[count].sprite = clickedTrue;
            }
            else
            {
                patternPlaying[count].sprite = clickedFalse;
            }
        }
    }

}
