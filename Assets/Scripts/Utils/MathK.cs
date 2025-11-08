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

    public static string FormatNumberWithSuffix(double number)
    {
        if (number < 1000)
        {
            // For numbers below 1000, show decimal only if not .0
            string format = (number % 1 == 0) ? "F0" : "F1";
            return number.ToString(format);
        }

        int suffixIndex = 0;
        // Use 999.5 threshold to handle floating-point precision issues
        while (number >= 999.5 && suffixIndex < suffixes.Length - 1)
        {
            number /= 1000.0;
            suffixIndex++;
        }
        
        // If we've run out of suffixes and number is still >= 1000, show it with the last suffix
        if (suffixIndex >= suffixes.Length - 1 && number >= 1000)
        {
            // We're at the max suffix, just show the large number with the last suffix
            return number.ToString("F1") + suffixes[suffixIndex];
        }
        
        // Show decimal only if not .0
        string numberFormat = (number % 1 == 0) ? "F0" : "F1";
        return number.ToString(numberFormat) + suffixes[suffixIndex];
    }
    
    public static float GetRandomSign()
    {
        return Random.value < 0.5f ? -1f : 1f;
    }
}
