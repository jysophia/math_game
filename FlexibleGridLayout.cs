using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlexibleGridLayout : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    private List<int> permutations;
    public List<GameObject> numbersOnGrid;
    public GameObject numberPrefab;

    protected override void Start()
    {
        ClearGrid();
        GetPermutationsFromGameManager();
    }

    void Update()
    {
        
    }

    void ClearGrid()
    {
        if (numbersOnGrid != null)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            numbersOnGrid.Clear();
        }
        
    }

    async void GetPermutationsFromGameManager()
    {
        GameManagerScript gameManager = FindFirstObjectByType<GameManagerScript>();
        if (gameManager != null)
        {
            while (gameManager.permutations == null || gameManager.permutations.Count == 0)
            {
                // Wait until permutations are generated
                await UniTask.Yield();
            }

            permutations = gameManager.permutations;
            for (int i = 0; i < permutations.Count; i++)
            {
                GameObject numberPrefab = Instantiate(this.numberPrefab, transform);
                numberPrefab.GetComponentInChildren<TextMeshProUGUI>().text = permutations[i].ToString();
                numbersOnGrid.Add(numberPrefab);
            }
        }
        else
        {
            Debug.LogError("GameManagerScript not found in the scene.");
        }
    }

    public override async void CalculateLayoutInputVertical()
    {
        while (permutations == null || permutations.Count == 0)
        {
            await UniTask.Yield();
        }
        // TODO: Implement layout calculation logic here
        base.CalculateLayoutInputHorizontal();
        int count = permutations.Count;
        rows = Mathf.CeilToInt(Mathf.Sqrt(count));
        columns = Mathf.CeilToInt((float)count / rows);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / columns - (spacing.x / columns * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = parentHeight / rows - (spacing.y / rows * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        for (int i = 0; i < numbersOnGrid.Count; i++)
        {
            int rowCount = i / columns;
            int columnCount = i % columns;

            var item = numbersOnGrid[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
            SetChildAlongAxis(item.GetComponent<RectTransform>(), 0, xPos, cellSize.x);
            SetChildAlongAxis(item.GetComponent<RectTransform>(), 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {
        // TODO: Implement horizontal layout logic here
    }

    public override void SetLayoutVertical()
    {
        // TODO: Implement vertical layout logic here
    }
}