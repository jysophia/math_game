using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestingInputSystem : MonoBehaviour, IPointerClickHandler
{
    private GameObject numberPrefab;
    private FlexibleGridLayout grid;
    private static List<int> multipleSelectedNumbers = new List<int>();
    private static Stack<GameObject> multipleSelectedNumberPrefabs = new Stack<GameObject>();
    private static int count;
    private int target;
    async void Start()
    {
        GameManagerScript gameManager = FindFirstObjectByType<GameManagerScript>();
        if (gameManager != null)
        {
            while (gameManager.permutations == null || gameManager.permutations.Count == 0)
            {
                // Wait until permutations are generated
                await UniTask.Yield();
            }

            target = gameManager.target;
        }
        else
        {
            Debug.LogError("GameManagerScript not found in InputSystem.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        numberPrefab = eventData.pointerCurrentRaycast.gameObject;
        var text = numberPrefab.GetComponentInChildren<TextMeshProUGUI>().text;
        var selectedNumber = int.Parse(text);
        multipleSelectedNumbers.Add(selectedNumber);
        multipleSelectedNumberPrefabs.Push(numberPrefab);
        count += selectedNumber;
    }

    public void Update()
    {
        if (count == target && multipleSelectedNumbers.Count >= 2)
        {
            Debug.Log("CORRECT!");
            count = 0;
            multipleSelectedNumbers.Clear();
            while (multipleSelectedNumberPrefabs.Count > 0)
            {
                var numberCellObject = multipleSelectedNumberPrefabs.Pop();
                if (grid != null && grid.cellTexts.Contains(numberCellObject))
                {
                    grid.cellTexts.Remove(numberCellObject);
                }
                Destroy(numberCellObject);
            }
        }
        else if (count != target && multipleSelectedNumbers.Count >= 2)
        {
            Debug.Log("WRONG!");
            count = 0;
            multipleSelectedNumbers.Clear();
        }
    }
}
