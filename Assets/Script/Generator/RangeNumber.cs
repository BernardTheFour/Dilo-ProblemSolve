using System;

[Serializable]
public class RangeNumber
{
    public float start;
    public float end;

    public RangeNumber(float start, float end)
    {
        this.start = start;
        this.end = end;
    }
}
