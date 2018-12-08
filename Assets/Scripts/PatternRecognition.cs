using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternRecognition : MonoBehaviour {

    public GameObject image;
    public Queue<float> patternQueue;

    [SerializeField] private int Bpm;
    private float secondsPerBeat;
    private float beatTimer = 0;
    [SerializeField] private bool startBeatTimer = true;
    
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
            if (beatTimer >= secondsPerBeat)
            {
                beatTimer -= secondsPerBeat;
                Debug.Log("Beat");
                StartCoroutine(ImageCoroutine());
            }
        }

	}

    private IEnumerator ImageCoroutine()
    {
        image.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        image.SetActive(false);
    }
}
