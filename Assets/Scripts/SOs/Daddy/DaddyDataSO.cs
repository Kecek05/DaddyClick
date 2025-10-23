using UnityEngine;

[CreateAssetMenu(fileName = "DaddyDataSO", menuName = "Scriptable Objects/Daddy/DaddyDataSO")]
public class DaddyDataSO : ScriptableObject
{
    public string Name;
    public DaddyType DaddyType;
    public Sprite Icon;
    [Tooltip("Can Leave Material null")]
    public Material Material;
    public int Stars;
    public float Multiplier;
}
