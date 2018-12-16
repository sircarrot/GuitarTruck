using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Basically the input manager
/// </summary>
public class PatternRecognition : MonoBehaviour {

    private GameManager gameManager;

    public PatternScriptableObject scriptableObject;

    public GameObject imageObject;

    public GameObject playerObject;
    public GameObject enemyObject;

    public float beatCounter = 0f;
    public string beatPatternString = "";

    [SerializeField]
    public Dictionary<string, Pattern> patternDictionary = new Dictionary<string, Pattern>();

    [SerializeField] private int Bpm;
    private float secondsPerBeat;
    private float accuracy = 0.35f;
    private float beatTimer = 0;
    [SerializeField] private bool startBeatTimer = true;
    [SerializeField] private bool startPattern;
    [SerializeField] private float startCounter;
    private float beatStep = 0.5f;
    private bool initComplete = false;

    public void Init()
    {
        secondsPerBeat = 60f / Bpm;
        gameManager = Toolbox.Instance.GetManager<GameManager>();
        foreach(Pattern pattern in scriptableObject.patternList)
        {
            string patternString = pattern.patternString.Trim(' ');
            patternDictionary.Add(patternString, pattern);
        }
        initComplete = true;
    }

    // Update is called once per frame
    void Update () {
        if (!initComplete) return;
        if (startBeatTimer)
        {
            beatTimer += Time.deltaTime;
            beatCounter += (Time.deltaTime / secondsPerBeat) / beatStep;

            if (beatTimer >= secondsPerBeat)
            {
                beatTimer -= secondsPerBeat;
                Debug.Log("Beat");
                StartCoroutine(BorderCoroutine());
                gameManager.BeatAnimation();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click");

                if (!startPattern)
                {
                    float beatTiming = beatCounter % 1;
                    beatTiming = (beatTiming < accuracy) ? beatTiming : beatTiming-1;
                    if(BeatRecognizer(beatTiming))
                    {
                        startPattern = true;
                        startCounter = Mathf.RoundToInt(beatCounter);
                        beatPatternString += "1";

                        CheckPattern();
                    }
                    else
                    {
                        Debug.Log("Fail");
                        ResetPattern();
                    }
                }
                else
                {
                    float beatTiming = beatCounter % 1;
                    beatTiming = (beatTiming < accuracy) ? beatTiming : beatTiming - 1;
                    if (BeatRecognizer(beatTiming))
                    {
                        int offset = (beatTiming > 0) ? -1 : 0;
                        beatPatternString = PatternFill(offset);
                        beatPatternString += "1";

                        CheckPattern();
                    }
                    else
                    {
                        Debug.Log("Fail Chain");
                        ResetPattern();
                    }
                }
            }

            gameManager.PatternPlayedUI(PatternFill());
        }
	}
    
    private void CheckPattern()
    {
        string pattern = PatternFill();
        Debug.Log("Length " + beatPatternString.Length);
        Debug.Log(beatPatternString);

        switch (CheckDictionary(pattern))
        {
            case PatternState.FoundPattern:
                Debug.Log("Found Pattern!");
                // Play pattern animation
                PlayPattern(pattern);
                ResetPattern();
                break;

            case PatternState.Valid:
                Debug.Log("Valid Pattern");
                break;

            case PatternState.Invalid:
                Debug.Log("Invalid Pattern, resetting");
                ResetPattern();
                break;
        }
    }

    private void PlayPattern(string pattern)
    {
        Pattern patternResult = patternDictionary[pattern];

        switch(patternResult.type)
        {
            case PatternType.Attack:
                gameManager.DamageEnemyHP(patternResult.damage);
                gameManager.DamageEnemyStagger(patternResult.stun);
                break;

            case PatternType.Defend:
                gameManager.SetDefend(patternResult);
                break;
        }

    }

    private string PatternFill(int offset = 0)
    {
        string result = beatPatternString;
        while (beatPatternString.Length - offset < (beatCounter - startCounter))
        {
            result += "0";
        }
        return result;
    }

    private void ResetPattern()
    {
        beatPatternString = "";
        startPattern = false;
        gameManager.ResetPattern();
    }

    private bool BeatRecognizer(float beatTiming)
    {
        if(beatTiming >= accuracy * -1  && beatTiming <= accuracy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///   TODO: Remove this and switch to UI Manager
    /// </summary>
    /// <returns></returns>
    private IEnumerator BorderCoroutine()
    {
        imageObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);
        imageObject.SetActive(false);
    }
    
    private PatternState CheckDictionary(string pattern)
    {
        if(patternDictionary.ContainsKey(pattern))
        {
            return PatternState.FoundPattern;
        }

        int count = 0;
        foreach(string key in patternDictionary.Keys)
        {
            string compareString = key.Substring(pattern.Length);
            if(compareString == pattern)
            {
                ++count;
            }
        }

        if(count > 0)
        {
            return PatternState.Valid;
        }

        return PatternState.Invalid;
    }

    private enum PatternState
    {
        Valid,
        Invalid,
        FoundPattern
    }
}
