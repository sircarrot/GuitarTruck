using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Pattern Scriptable Object")]
public class PatternScriptableObject : ScriptableObject {

    public List<Pattern> patternList = new List<Pattern>();

}

