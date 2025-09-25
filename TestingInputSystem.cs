using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestingInputSystem : MonoBehaviour, IPointerClickHandler
{
    private GameObject numberPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        numberPrefab = eventData.pointerCurrentRaycast.gameObject;
        var text = numberPrefab.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log("Clicked on " + text);
    }
}
