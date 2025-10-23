using UnityEngine;

public static class MathK
{
    private static string[] suffixes = { 
        "", "K", "M", "B", "T", 
        "Qa", "Qi", "Sx", "Sp", "Oc", 
        "No", "Dc", "UDc", "DDc", "TDc", 
        "QaDc", "QiDc", "SxDc", "SpDc", "OcDc" 
    };
    
    public static float RoundToInt(float number)
    {
        return Mathf.RoundToInt(number);
    }

    public static string FormatNumberWithSuffix(float number)
    {
        if (number < 1000)
            return number.ToString("F0");

        int suffixIndex = 0;
        while (number >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            number /= 1000f;
            suffixIndex++;
        }
        return number.ToString("F1") + suffixes[suffixIndex];
    }
}
