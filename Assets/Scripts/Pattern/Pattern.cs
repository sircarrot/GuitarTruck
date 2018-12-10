[System.Serializable]
public class Pattern
{
    public string patternString;
    public PatternType type;
    public int damage;
    public int stun;
}

public enum PatternType
{
    Type1,
    Type2
}

