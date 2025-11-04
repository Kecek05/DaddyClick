using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickManager.ManualClick(1 + ClickManager.CPS, eventData.position);
    }
    
}

