[System.Serializable]
public class Pattern
{
    public string patternString;
    public PatternType type;
    public PatternColor color;
    public int damage;
    public int stun;
}

public enum PatternType
{
    Attack,
    Defend
}

public enum PatternColor
{
    Red,
    Blue,
}

