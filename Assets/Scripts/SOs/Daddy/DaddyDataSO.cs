using UnityEngine;

[CreateAssetMenu(fileName = "DaddyDataSO", menuName = "Scriptable Objects/Daddy/DaddyDataSO")]
public class DaddyDataSO : ScriptableObject
{
    public string Name;
    public DaddyType DaddyType;
    public Sprite Icon;
    public int Stars;
    public float Multiplier;
}
