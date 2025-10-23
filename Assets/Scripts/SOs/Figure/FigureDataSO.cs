using UnityEngine;

[CreateAssetMenu(fileName = "FigureDataSO", menuName = "Scriptable Objects/Figure/FigureDataSO")]
public class FigureDataSO : ScriptableObject
{
    public string Name;
    public FigureType FigureType;
    public Sprite Icon;
    public int Stars;
    public float CPS;
}
