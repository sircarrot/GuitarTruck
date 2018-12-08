using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternRecognition : MonoBehaviour {

    public GameObject imageObject;
    public float beatCounter = 0f;
    public string beatPatternString = "";

    private Dictionary<string, string> patternDictionary = new Dictionary<string, string>();

    [SerializeField] private int Bpm;
    private float secondsPerBeat;
    private float accuracy = 0.35f;
    private float beatTimer = 0;
    [SerializeField] private bool startBeatTimer = true;
    [SerializeField] private bool startPattern;
    [SerializeField] private float startCounter;
    private float beatStep = 0.5f;

	// Use this for initialization
	void Start ()
    {
        secondsPerBeat = 60f / Bpm;
        

	}
	
	// Update is called once per frame
	void Update () {
        if (startBeatTimer)
        {
            beatTimer += Time.deltaTime;
            beatCounter += (Time.deltaTime / secondsPerBeat) / beatStep;

            if (beatTimer >= secondsPerBeat)
            {
                beatTimer -= secondsPerBeat;
                Debug.Log("Beat");
                StartCoroutine(ImageCoroutine());
            }

            if(startPattern && (beatCounter - startCounter) * beatStep >= 3)
            {
                Debug.Log("Check Pattern");

                CheckPattern();
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

    private IEnumerator ImageCoroutine()
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
