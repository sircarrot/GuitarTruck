using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager {

    private PatternRecognition patternRecognition;

    public void Init()
    {
        patternRecognition = gameObject.GetComponent<PatternRecognition>();

        patternRecognition.Init();
    }




}
