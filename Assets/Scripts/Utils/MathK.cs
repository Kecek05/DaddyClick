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
        {
            // For numbers below 1000, show decimal only if not .0
            string format = (number % 1 == 0) ? "F0" : "F1";
            return number.ToString(format);
        }

        int suffixIndex = 0;
        while (number >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            number /= 1000f;
            suffixIndex++;
        }
        
        // Show decimal only if not .0
        string numberFormat = (number % 1 == 0) ? "F0" : "F1";
        return number.ToString(numberFormat) + suffixes[suffixIndex];
    }
}
