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

    private static int numberPrefabsClearedSoFar = 0;
    private static int sumSoFar;
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
            grid = FindFirstObjectByType<FlexibleGridLayout>();
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
        sumSoFar += selectedNumber;
    }

    public void Update()
    {
        if (sumSoFar == target && multipleSelectedNumbers.Count >= 2)
        {
            Debug.Log("CORRECT!");
            sumSoFar = 0;
            multipleSelectedNumbers.Clear();
            while (multipleSelectedNumberPrefabs.Count > 0)
            {
                var numberPrefabOfInterest = multipleSelectedNumberPrefabs.Pop();
                if (grid != null && grid.numbersOnGrid.Contains(numberPrefabOfInterest))
                {
                    int indexOfNumberPrefabOfInterest = grid.numbersOnGrid.IndexOf(numberPrefabOfInterest);
                    grid.numbersOnGrid[indexOfNumberPrefabOfInterest].GetComponentInChildren<TextMeshProUGUI>().text = "";
                    numberPrefabsClearedSoFar++;
                }
            }
            if (numberPrefabsClearedSoFar >= grid.numbersOnGrid.Count)
            {
                Debug.Log("CLEAR!");
                numberPrefabsClearedSoFar = 0;
            }
        }
        else if (sumSoFar != target && multipleSelectedNumbers.Count >= 2)
        {
            Debug.Log("WRONG!");
            sumSoFar = 0;
            multipleSelectedNumbers.Clear();
            multipleSelectedNumberPrefabs.Clear();
        }
    }
}
