using UnityEngine;

public static class MathK
{
    public static float FormatNumber(float number)
    {
        return Mathf.Round(number * 100f) / 100f;
    }
}
