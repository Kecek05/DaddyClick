using UnityEngine;

[CreateAssetMenu(fileName = "FigureDataSO", menuName = "Scriptable Objects/FigureDataSO")]
public class FigureDataSO : ScriptableObject
{
    public string Name;
    public FigureType FigureType;
    public Sprite Icon;
    public float CPS;
}
