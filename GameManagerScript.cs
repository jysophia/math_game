using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int target;
    public List<int> permutations;
    public int gridWidth = 2;
    public int gridHeight = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTargetAndPermutations();
    }

    void GenerateTargetAndPermutations()
    {
        target = Random.Range(4, 100);
        permutations = new List<int>();
        for (int i = 0; i < gridHeight; i++)
        {
            int randomNum = Random.Range(1, target);
            while (permutations.Contains(randomNum))
            {
                randomNum = Random.Range(1, target);
            }
            permutations.Add(randomNum);
            permutations.Add(target - randomNum);
        }
        Debug.Log("Permutations: " + string.Join(", ", permutations));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
