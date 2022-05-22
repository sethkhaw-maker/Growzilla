using System;
using System.Collections.Generic;

[Serializable]
public class KaijuuData : IComparable<KaijuuData>
{
    public string KaijuuName;

    public int FoodEaten;
    public float[] Color;

    public string Ability1;
    public string Ability2;

    public int Destruction;
    public int Distance;
    public int Score;

    public int CompareTo(KaijuuData other)
    {
        if (this.Score > other.Score)
            return 1;
        else if (this.Score < other.Score)
            return -1;
        else
            return 0;
    }
}

[Serializable]
public class CacheData
{
    public KaijuuData Current = new KaijuuData();
}
[Serializable]
public class DynamicData
{
    public List<KaijuuData> Highscore = new List<KaijuuData>();
}
