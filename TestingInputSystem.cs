using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    private GameObject numberPrefab;
    private PlayerInput playerInput;

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
    
    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = mousePos
            };

            var raycastResult = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResult);

            foreach (var result in raycastResult)
            {
                if (result.gameObject.GetComponentInChildren<NumberCellScript>())
                {
                    numberPrefab = result.gameObject;
                    Debug.Log("Clicked on: " + numberPrefab.GetComponent<TextMeshProUGUI>().text);
                }
            }
        }

    }
}
